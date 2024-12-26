using AppleShop.product.commandApplication.Commands.Color;
using AppleShop.product.commandApplication.Commands.Product;
using AppleShop.product.commandApplication.Commands.ProductImage;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppleShop.product.commandApi.MinimalApis
{
    public static class EndpointRegistration
    {
        #region Product API
        public static IEndpointRouteBuilder ProductAction(this IEndpointRouteBuilder builder)
        {
            var product = builder.MapGroup("/").WithTags("Product");
            product.MapPost("/create-product", async ([FromBody] CreateProductCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            product.MapPut("/update-product/{id}", async (int? id, [FromBody] UpdateProductCommand command, IMediator mediator) =>
            {
                command.Id = id;
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            return builder;
        }
        #endregion

        #region Product Image API
        public static IEndpointRouteBuilder ProductImageAction(this IEndpointRouteBuilder builder)
        {
            var productImage = builder.MapGroup("/").WithTags("ProductImage");
            productImage.MapPost("/create-product-img", async ([FromBody] CreateProductImageCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            productImage.MapPut("/update-product-img/{id}", async (int? id, [FromBody] UpdateProductImageCommand command, IMediator mediator) =>
            {
                command.Id = id;
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            productImage.MapDelete("/delete-product-img/{id}", async (int? id, IMediator mediator) =>
            {
                var command = new DeleteProductImageCommand();
                command.Id = id;
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            return builder;
        }
        #endregion

        #region Color API
        public static IEndpointRouteBuilder ColorAction(this IEndpointRouteBuilder builder)
        {
            var color = builder.MapGroup("/").WithTags("Color");
            color.MapPost("/create-color", async ([FromBody] CreateColorCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            color.MapPut("/update-color/{id}", async (int? id, [FromBody] UpdateColorCommand command, IMediator mediator) =>
            {
                command.Id = id;
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            color.MapDelete("/delete-color/{id}", async (int? id, IMediator mediator) =>
            {
                var command = new DeleteColorCommand();
                command.Id = id;
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            return builder;
        }
        #endregion
    }
}