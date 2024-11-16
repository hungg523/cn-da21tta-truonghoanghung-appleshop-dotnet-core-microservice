using AppleShop.commandApplication.Commands.ProductImage;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppleShop.commandApi.MinimalApis
{
    public static class ProductImageMinimalApi
    {
        public static IEndpointRouteBuilder ProductImageAction(this IEndpointRouteBuilder builder)
        {
            var productImage = builder.MapGroup("/product-image").WithTags("ProductImage");
            productImage.MapPost("/create", async ([FromBody] CreateProductImageCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            productImage.MapPut("/update/{id}", async (int? id, [FromBody] UpdateProductImageCommand command, IMediator mediator) =>
            {
                command.Id = id;
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            productImage.MapDelete("/delete/{id}", async (int? id, IMediator mediator) =>
            {
                var command = new UpdateProductImageCommand();
                command.Id = id;
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            return builder;
        }
    }
}