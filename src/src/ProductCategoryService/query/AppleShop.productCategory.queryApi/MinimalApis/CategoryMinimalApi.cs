using AppleShop.productCategory.queryApplication.Queries.Category;
using MediatR;

namespace AppleShop.productCategory.queryApi.MinimalApis
{
    public static class CategoryMinimalApi
    {
        public static IEndpointRouteBuilder CategoryAction(this IEndpointRouteBuilder builder)
        {
            var category = builder.MapGroup("/category").WithTags("Category"); ;
            category.MapGet("/get-all", async (IMediator mediator) =>
            {
                var command = new GetAllCategoriesQuery();
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            category.MapGet("/get-by-id/{id}", async (int? id, IMediator mediator) =>
            {
                var command = new GetCategoryByIdQuery();
                command.Id = id;
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            return builder;
        }
    }
}