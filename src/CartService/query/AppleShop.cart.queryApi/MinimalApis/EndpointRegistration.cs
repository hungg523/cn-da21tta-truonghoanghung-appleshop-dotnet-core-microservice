using AppleShop.cart.queryApplication.Queries.Cart;
using MediatR;

namespace AppleShop.cart.queryApi.MinimalApis
{
    public static class EndpointRegistration
    {
        #region Cart API
        public static IEndpointRouteBuilder CartAction(this IEndpointRouteBuilder builder)
        {
            var cart = builder.MapGroup("/").WithTags("Cart"); ;

            cart.MapGet("/get-cart-by-user-id/{id}", async (int? id, IMediator mediator) =>
            {
                var query = new GetCartByUserIdQuery();
                query.UserId = id;
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });

            return builder;
        }
        #endregion
    }
}