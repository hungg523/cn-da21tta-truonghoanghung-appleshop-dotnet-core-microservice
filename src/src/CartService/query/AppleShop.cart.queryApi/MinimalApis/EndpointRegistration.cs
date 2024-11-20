using AppleShop.cart.queryApplication.Queries.Cart;
using MediatR;

namespace AppleShop.cart.queryApi.MinimalApis
{
    public static class EndpointRegistration
    {
        #region Cart API
        public static IEndpointRouteBuilder CategoryAction(this IEndpointRouteBuilder builder)
        {
            var cart = builder.MapGroup("/cart").WithTags("Cart"); ;

            cart.MapGet("/get-by-user-id/{id}", async (int? id, IMediator mediator) =>
            {
                var command = new GetCartByUserIdQuery();
                command.UserId = id;
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            return builder;
        }
        #endregion
    }
}