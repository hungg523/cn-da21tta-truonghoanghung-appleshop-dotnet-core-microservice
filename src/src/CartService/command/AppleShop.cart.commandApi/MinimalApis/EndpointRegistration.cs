using AppleShop.cart.commandApplication.Commands.Cart;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppleShop.cart.commandApi.MinimalApis
{
    public static class EndpointRegistration
    {
        #region Cart API
        public static IEndpointRouteBuilder CartAction(this IEndpointRouteBuilder builder)
        {
            var cart = builder.MapGroup("/cart").WithTags("Cart");
            cart.MapPost("/create", async ([FromBody] CreateCartCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            cart.MapDelete("/delete/{cartId}/{productId}", async (int? cartId, int? productId, IMediator mediator) =>
            {
                var command = new DeleteProductFromCartItemCommand();
                command.CartId = cartId;
                command.ProductId = productId;
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            return builder;
        }
        #endregion
    }
}