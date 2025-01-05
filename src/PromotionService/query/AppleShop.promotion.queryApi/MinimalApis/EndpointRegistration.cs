using AppleShop.promotion.queryApplication.Queries.Promotion;
using MediatR;

namespace AppleShop.promotion.queryApi.MinimalApis
{
    public static class EndpointRegistration
    {
        #region Promotion API
        public static IEndpointRouteBuilder PromotionAction(this IEndpointRouteBuilder builder)
        {
            var order = builder.MapGroup("/").WithTags("Promotion"); ;

            order.MapGet("/get-all-promotions", async (IMediator mediator) =>
            {
                var query = new GetAllPromotionQuery();
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });

            order.MapGet("/get-promotion-by-id/{id}", async (int? id, IMediator mediator) =>
            {
                var query = new GetPromotionByIdQuery();
                query.Id = id;
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });

            return builder;
        }
        #endregion
    }
}