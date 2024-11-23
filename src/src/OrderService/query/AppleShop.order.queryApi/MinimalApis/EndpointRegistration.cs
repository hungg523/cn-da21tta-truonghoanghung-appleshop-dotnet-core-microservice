using AppleShop.order.queryApplication.Queries.Order;
using MediatR;

namespace AppleShop.order.queryApi.MinimalApis
{
    public static class EndpointRegistration
    {
        #region Order API
        public static IEndpointRouteBuilder OrderAction(this IEndpointRouteBuilder builder)
        {
            var order = builder.MapGroup("/order").WithTags("Order"); ;

            order.MapGet("/get-all", async (IMediator mediator) =>
            {
                var command = new GetAllOrderQuery();
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            order.MapGet("/get-by-user-id/{id}", async (int? id, IMediator mediator) =>
            {
                var command = new GetOrderByUserIdQuery();
                command.UserId = id;
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            return builder;
        }
        #endregion
    }
}