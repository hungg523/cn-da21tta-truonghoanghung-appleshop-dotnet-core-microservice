using AppleShop.product.queryApplication.Queries.Product;
using MediatR;

namespace AppleShop.product.queryApi.MinimalApis
{
    public static class EndpointRegistration
    {
        #region Product API
        public static IEndpointRouteBuilder ProductAction(this IEndpointRouteBuilder builder)
        {
            var product = builder.MapGroup("/").WithTags("Product");
            product.MapGet("/get-all-products", async (IMediator mediator) =>
            {
                var query = new GetAllProductQuery();
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });

            product.MapGet("/get-all-products-actived", async (IMediator mediator) =>
            {
                var query = new GetAllProductsActivedQuery();
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });

            product.MapGet("/get-product-by-id/{id}", async (int? id, IMediator mediator) =>
            {
                var query = new GetProductByIdQuery();
                query.Id = id;
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });

            product.MapGet("/get-product-by-category-id/{id}", async (int? id, IMediator mediator) =>
            {
                var query = new GetAllProductByCategoryIdQuery();
                query.CategoryId = id;
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });

            product.MapGet("/get-product-by-name/{name}", async (string? name, IMediator mediator) =>
            {
                var query = new GetAllProductsActivedByNameQuery();
                query.Name = name;
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });

            return builder;
        }
        #endregion
    }
}