using AppleShop.order.Domain.Abstractions.IRepositories;
using AppleShop.order.queryApplication.Queries.DTOs;
using AppleShop.order.queryApplication.Queries.Order;
using AppleShop.Share.Enumerations;
using AppleShop.Share.Events.Cart.Query;
using AppleShop.Share.Exceptions;
using AppleShop.Share.Shared;
using MassTransit;
using MassTransit.Clients;
using MediatR;
using Entities = AppleShop.order.Domain.Entities;

namespace AppleShop.order.queryApplication.Handler.Order
{
    public class GetAllOrderQueryHandler : IRequestHandler<GetAllOrderQuery, Result<List<OrderFullDTO>>>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IRequestClient<GetAllCartItemByUserIdEvent> requestClient;
        private readonly IOrderItemRepository orderItemRepository;

        public GetAllOrderQueryHandler(IOrderRepository orderRepository, IRequestClient<GetAllCartItemByUserIdEvent> requestClient, IOrderItemRepository orderItemRepository)
        {
            this.orderRepository = orderRepository;
            this.requestClient = requestClient;
            this.orderItemRepository = orderItemRepository;
        }

        public async Task<Result<List<OrderFullDTO>>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
        {
            var orders = orderRepository.FindAll(includeProperties: o => o.OrderItems).ToList();
            if (orders is null) AppleException.ThrowNotFound(typeof(Entities.Order));

            var productIds = orders.SelectMany(o => o.OrderItems).Select(x => x.ProductId).ToList();
            if (productIds is null || !productIds.Any()) AppleException.ThrowNotFound(message: "No products found in the order.");

            var productsResponse = requestClient.GetResponse<ProductsResponse>(new GetAllCartItemByUserIdEvent { ProductIds = productIds }, cancellationToken);
            var products = productsResponse.Result.Message.Products.ToDictionary(p => p.ProductId, p => p);

            var orderDtos = orders.Select(order => new OrderFullDTO
            {
                Id = order.Id,
                Status = order.OrderStatus,
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
            }).ToList();
            return Result<List<OrderFullDTO>>.Ok(orderDtos);
        }
    }
}