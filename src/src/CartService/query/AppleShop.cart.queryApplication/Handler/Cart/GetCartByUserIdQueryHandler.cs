using AppleShop.cart.Domain.Abstractions.IRepositories;
using AppleShop.cart.queryApplication.Queries.Cart;
using AppleShop.cart.queryApplication.Queries.DTOs;
using AppleShop.cart.queryApplication.Validator.Cart;
using AppleShop.Share.Events.Cart.Query;
using AppleShop.Share.Exceptions;
using AppleShop.Share.Shared;
using MassTransit;
using MediatR;
using Entities = AppleShop.cart.Domain.Entities;

namespace AppleShop.cart.queryApplication.Handler.Cart
{
    public class GetCartByUserIdQueryHandler : IRequestHandler<GetCartByUserIdQuery, Result<CartFullDTO>>
    {
        private readonly ICartRepository cartRepository;
        private readonly IRequestClient<GetAllCartItemByUserIdEvent> requestClient;
        private readonly ICartItemRepository cartItemRepository;

        public GetCartByUserIdQueryHandler(ICartRepository cartRepository, IRequestClient<GetAllCartItemByUserIdEvent> requestClient, ICartItemRepository cartItemRepository)
        {
            this.cartRepository = cartRepository;
            this.requestClient = requestClient;
            this.cartItemRepository = cartItemRepository;
        }

        public async Task<Result<CartFullDTO>> Handle(GetCartByUserIdQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetCartByUserIdQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            var cart = await cartRepository.FindSingleAsync(x => x.UserId == request.UserId, false, includeProperties: c => c.CartItems);
            if (cart is null) AppleException.ThrowNotFound(typeof(Entities.Cart));

            var productIds = cart.CartItems.Select(x => x.ProductId).ToList();
            if (productIds is null || !productIds.Any()) AppleException.ThrowNotFound(message: "No products found in the cart.");

            var productsResponse = requestClient.GetResponse<ProductsResponse>(new GetAllCartItemByUserIdEvent { ProductIds = productIds }, cancellationToken);
            var products = productsResponse.Result.Message.Products.ToDictionary(p => p.ProductId, p => p);

            var cartDto = new CartFullDTO
            {
                Id = cart.Id,
                CartItems = cart.CartItems?.Select(ci => new CartItemDTO
                {
                    ProductId = ci.ProductId ?? 0,
                    Name = products.ContainsKey(ci.ProductId ?? 0) ? products[ci.ProductId.Value].Name : null,
                    Description = products.ContainsKey(ci.ProductId ?? 0) ? products[ci.ProductId.Value].Description : null,
                    Quantity = ci.Quantity ?? 0,
                    UnitPrice = ci.UnitPrice ?? 0,
                    TotalPrice = (ci.UnitPrice ?? 0) * (ci.Quantity ?? 0),
                }).ToList(),
                TotalPrice = cart.CartItems?.Sum(ci => (ci.UnitPrice ?? 0) * (ci.Quantity ?? 0)) ?? 0
            };
            return Result<CartFullDTO>.Ok(cartDto);
        }
    }
}