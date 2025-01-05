using AppleShop.order.queryApplication.Queries.Order;
using MediatR;

namespace AppleShop.order.queryApi.MinimalApis
{
    public static class EndpointRegistration
    {
        #region Order API
        public static IEndpointRouteBuilder OrderAction(this IEndpointRouteBuilder builder)
        {
            var order = builder.MapGroup("/").WithTags("Order"); ;

            order.MapGet("/get-all-orders", async (IMediator mediator) =>
            {
                var query = new GetAllOrderQuery();
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });

            order.MapGet("/get-order-by-id/{id}", async (int? id, IMediator mediator) =>
            {
                var query = new GetOrderByIdQuery();
                query.Id = id;
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });

            order.MapGet("/get-order-by-user-id/{id}", async (int? id, IMediator mediator) =>
            {
                var query = new GetOrderByUserIdQuery();
                query.UserId = id;
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });

            return builder;
        }
        #endregion
    }
}