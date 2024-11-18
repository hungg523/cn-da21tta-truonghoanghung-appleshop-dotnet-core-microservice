using AppleShop.productCategory.commandApplication.Commands.Product;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppleShop.productCategory.commandApi.MinimalApis
{
    public static class ProductMinimalApis
    {
        public static IEndpointRouteBuilder ProductAction(this IEndpointRouteBuilder builder)
        {
            var product = builder.MapGroup("/product").WithTags("Product");
            product.MapPost("/create", async ([FromBody] CreateProductCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            product.MapPut("/update/{id}", async (int? id, [FromBody] UpdateProductCommand command, IMediator mediator) =>
            {
                command.Id = id;
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            return builder;
        }
    }
}