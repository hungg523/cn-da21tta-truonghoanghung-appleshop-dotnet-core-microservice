using AppleShop.order.Domain.Abstractions.IRepositories;
using AppleShop.order.queryApplication.Queries.DTOs;
using AppleShop.order.queryApplication.Queries.Order;
using AppleShop.order.queryApplication.Validator.Order;
using AppleShop.Share.Events.Cart.Response;
using AppleShop.Share.Events.Promotion.Request;
using AppleShop.Share.Events.Promotion.Response;
using AppleShop.Share.Events.User.Request;
using AppleShop.Share.Events.User.Response;
using AppleShop.Share.Exceptions;
using AppleShop.Share.Shared;
using MassTransit;
using MediatR;
using Entities = AppleShop.order.Domain.Entities;

namespace AppleShop.order.queryApplication.Handler.Order
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Result<OrderFullDTO>>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IRequestClient<GetAllCartItemByUserIdEvent> cartRequestClient;
        private readonly IOrderItemRepository orderItemRepository;
        private readonly IRequestClient<GetInfoUserEvent> userRequestClient;
        private readonly IRequestClient<GetInfoPromotionEvent> promotionRequestClient;

        public GetOrderByIdQueryHandler(IOrderRepository orderRepository,
                                          IRequestClient<GetAllCartItemByUserIdEvent> cartRequestClient,
                                          IOrderItemRepository orderItemRepository,
                                          IRequestClient<GetInfoUserEvent> userRequestClient,
                                          IRequestClient<GetInfoPromotionEvent> promotionRequestClient)
        {
            this.orderRepository = orderRepository;
            this.cartRequestClient = cartRequestClient;
            this.orderItemRepository = orderItemRepository;
            this.userRequestClient = userRequestClient;
            this.promotionRequestClient = promotionRequestClient;
        }

        public async Task<Result<OrderFullDTO>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetOrderByIdQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            var order = await orderRepository.FindSingleAsync(x => x.Id == request.Id, false, cancellationToken, oi => oi.OrderItems);
            if (order is null) AppleException.ThrowNotFound(typeof(Entities.Order));

            var productIds = order.OrderItems.Select(x => x.ProductId).ToList();
            if (productIds is null || !productIds.Any()) AppleException.ThrowNotFound(message: "No products found in the order.");

            var productsResponse = cartRequestClient.GetResponse<ProductsResponse>(new GetAllCartItemByUserIdEvent { ProductIds = productIds }, cancellationToken);
            var products = productsResponse.Result.Message.Products.ToDictionary(p => p.ProductId, p => p);

            var userRequest = new GetInfoUserEvent
            {
                UserId = new List<int?> { order.UserId },
                UserAddressId = new List<int?> { order.UserAddressId }
            };
            var userResponse = await userRequestClient.GetResponse<GetInfoUserResponse>(userRequest, cancellationToken);
            var users = userResponse.Message;

            var promotionRequest = new GetInfoPromotionEvent { Id = new List<int?> { order.PromotionId } };
            var promotionResponse = await promotionRequestClient.GetResponse<GetInfoPromotionResponse>(promotionRequest, cancellationToken);
            var promotions = promotionResponse.Message;

            var orderDto = new OrderFullDTO
            {
                Id = order.Id,
                Users = users.Users?.Select(u => new UserDTO
                {
                    Id = u.Id,
                    Email = u.Email
                }).ToList(),
                Status = order.OrderStatus,
                UserAddresses = users.UserAddresses?.Select(ua => new UserAddressDTO
                {
                    Id = ua.UserAddressId,
                    FullName = $"{ua.FirstName} {ua.LastName}",
                    PhoneNumber = ua.PhoneNumber,
                    Address = $"{ua.AddressLine} {ua.District} {ua.Province}"
                }).ToList(),
                Promotions = promotions.Promotions?.Select(pm => new PromotionDTO
                {
                    Id = pm.Id,
                    DiscountPercentage = pm.DiscountPercentage,
                    Description = pm.Discription
                }).ToList(),
                OrderItems = order.OrderItems?.Select(oi => new OrderItemDTO
                {
                    ProductId = oi.ProductId ?? 0,
                    Name = products.ContainsKey(oi.ProductId ?? 0) ? products[oi.ProductId.Value].Name : null,
                    Description = products.ContainsKey(oi.ProductId ?? 0) ? products[oi.ProductId.Value].Description : null,
                    Quantity = oi.Quantity ?? 0,
                    UnitPrice = oi.UnitPrice ?? 0,
                    TotalPrice = (oi.UnitPrice ?? 0) * (oi.Quantity ?? 0),
                }).ToList(),
                TotalAmount = order.TotalAmount ?? 0,
            };
            return Result<OrderFullDTO>.Ok(orderDto);
        }
    }
}