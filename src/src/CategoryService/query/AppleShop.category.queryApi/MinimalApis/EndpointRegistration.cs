using AppleShop.category.queryApplication.Queries.Category;
using MediatR;

namespace AppleShop.category.queryApi.MinimalApis
{
    public static class EndpointRegistration
    {
        #region Category API
        public static IEndpointRouteBuilder CategoryAction(this IEndpointRouteBuilder builder)
        {
            var category = builder.MapGroup("/").WithTags("Category"); ;
            category.MapGet("/get-all-categories", async (IMediator mediator) =>
            {
                var query = new GetAllCategoriesQuery();
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });

            category.MapGet("/get-all-categories-actived", async (IMediator mediator) =>
            {
                var query = new GetAllCategoriesActivedQuery();
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });

            category.MapGet("/get-category-by-id/{id}", async (int? id, IMediator mediator) =>
            {
                var query = new GetCategoryByIdQuery();
                query.Id = id;
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });

            return builder;
        }
        #endregion
    }
}