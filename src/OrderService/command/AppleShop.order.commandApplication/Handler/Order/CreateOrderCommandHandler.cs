using AppleShop.order.commandApplication.Commands.Order;
using AppleShop.order.commandApplication.Validator.Order;
using AppleShop.order.Domain.Abstractions.IRepositories;
using AppleShop.Share.Enumerations;
using AppleShop.Share.Events.Cart.Response;
using AppleShop.Share.Events.Inventory.Request;
using AppleShop.Share.Events.Inventory.Response;
using AppleShop.Share.Events.Promotion.Request;
using AppleShop.Share.Events.Promotion.Response;
using AppleShop.Share.Exceptions;
using AppleShop.Share.Shared;
using AutoMapper;
using MassTransit;
using MediatR;
using Entities = AppleShop.order.Domain.Entities;

namespace AppleShop.order.commandApplication.Handler.Order
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<object>>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;
        private readonly IOrderItemRepository orderItemRepository;
        private readonly IRequestClient<GetProductByIdEvent> productRequestClient;
        private readonly IRequestClient<CheckPromotionCodeExistEvent> promotionRequestClient;
        private readonly IRequestClient<CheckStockEvent> inventoryRequestClient;

        public CreateOrderCommandHandler(IOrderRepository orderRepository,
                                         IMapper mapper,
                                         IOrderItemRepository orderItemRepository,
                                         IRequestClient<GetProductByIdEvent> productRequestClient,
                                         IRequestClient<CheckPromotionCodeExistEvent> promotionRequestClient,
                                         IRequestClient<CheckStockEvent> inventoryRequestClient)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
            this.orderItemRepository = orderItemRepository;
            this.productRequestClient = productRequestClient;
            this.promotionRequestClient = promotionRequestClient;
            this.inventoryRequestClient = inventoryRequestClient;
        }

        public async Task<Result<object>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateOrderCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            using var transaction = await orderRepository.BeginTransactionAsync(cancellationToken);
            try
            {
                var order = new Entities.Order
                {
                    OrderStatus = OrderStatus.PENDING.ToString(),
                    Payment = Payment.COD.ToString(),
                    UserId = request.UserId,
                    UserAddressId = request.UserAddressId,
                    CreatedAt = DateTime.Now,
                    TotalAmount = 0,
                };

                orderRepository.Create(order);
                await orderRepository.SaveChangesAsync(cancellationToken);

                var orderItemTasks = request.OrderItems.Select(async item =>
                {
                    var productRequest = new GetProductByIdEvent { ProductId = item.ProductId };
                    var productResponse = await productRequestClient.GetResponse<ProductResponse>(productRequest, cancellationToken);
                    var product = productResponse.Message;
                    if (product.ProductId is null) AppleException.ThrowNotFound(message: "Product is not found.");

                    var inventoryRequest = new CheckStockEvent
                    {
                        ProductId = product.ProductId,
                        Stock = item.Quantity
                    };
                    var inventoryResponse = await inventoryRequestClient.GetResponse<CheckStockResponse>(inventoryRequest, cancellationToken);
                    var inventory = inventoryResponse.Message;
                    if (!inventory.Success) AppleException.ThrowConflict("Stock quantity is not enough.");

                    decimal price = product.DiscountPrice.HasValue && product.DiscountPrice > 0 ? product.DiscountPrice.Value : product.Price ?? 0;

                    return new Entities.OrderItem
                    {
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = price,
                        TotalPrice = price * item.Quantity
                    };
                });

                var orderItems = await Task.WhenAll(orderItemTasks);

                decimal? totalAmount = 0;
                foreach (var orderItem in orderItems)
                {
                    totalAmount += orderItem.TotalPrice;
                    orderItemRepository.Create(orderItem);
                }

                await orderItemRepository.SaveChangesAsync(cancellationToken);

                if (request.PromotionCode is not null)
                {
                    var promotionRequest = new CheckPromotionCodeExistEvent { Code = request.PromotionCode };
                    var promotionResponse = await promotionRequestClient.GetResponse<CheckPromotionCodeExistResponse>(promotionRequest, cancellationToken);
                    var promotion = promotionResponse.Message;
                    if (promotion.PromotionId is null) AppleException.ThrowNotFound(message: "Promotion Code is invalid.");
                    
                    order.PromotionId = promotion.PromotionId;

                    decimal? discount = (totalAmount * promotion.DiscountPercentage) / 100;
                    totalAmount -= discount;

                    if (totalAmount < 0) totalAmount = 0;
                }

                order.TotalAmount = totalAmount;
                orderRepository.Update(order);
                await orderRepository.SaveChangesAsync(cancellationToken);
                transaction.Commit();
                return Result<object>.Ok();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}