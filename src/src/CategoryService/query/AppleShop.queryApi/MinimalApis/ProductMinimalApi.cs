using AppleShop.queryApplication.Queries.Product;
using MediatR;

namespace AppleShop.queryApi.MinimalApis
{
    public static class ProductMinimalApi
    {
        public static IEndpointRouteBuilder ProductAction(this IEndpointRouteBuilder builder)
        {
            var product = builder.MapGroup("/product").WithTags("Product");
            product.MapGet("/get-all", async (IMediator mediator) =>
            {
                var command = new GetAllProductQuery();
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            product.MapGet("/get-by-id/{id}", async (int? id, IMediator mediator) =>
            {
                var command = new GetProductByIdQuery();
                command.Id = id;
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            product.MapGet("/get-by-category-id/{id}", async (int? id, IMediator mediator) =>
            {
                var command = new GetAllProductByCategoryIdQuery();
                command.CategoryId = id;
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            return builder;
        }
    }
}