using AppleShop.productCategory.commandApplication.Commands.Category;
using AppleShop.productCategory.commandApplication.Commands.Color;
using AppleShop.productCategory.commandApplication.Commands.Product;
using AppleShop.productCategory.commandApplication.Commands.ProductImage;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppleShop.productCategory.commandApi.MinimalApis
{
    public static class EndpointRegistration
    {
        #region Category API
        public static IEndpointRouteBuilder CategoryAction(this IEndpointRouteBuilder builder)
        {
            var category = builder.MapGroup("/category").WithTags("Category");
            category.MapPost("/create", async ([FromBody]CreateCategoryCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            category.MapPut("/update/{id}", async (int? id, [FromBody]UpdateCategoryCommand command, IMediator mediator) =>
            {
                command.Id = id;
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            return builder;
        }
        #endregion

        #region Product API
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
        #endregion

        #region Product Image API
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
            var color = builder.MapGroup("/color").WithTags("Color");
            color.MapPost("/create", async ([FromBody] CreateColorCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            color.MapPut("/update/{id}", async (int? id, [FromBody] UpdateColorCommand command, IMediator mediator) =>
            {
                command.Id = id;
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            color.MapDelete("/delete/{id}", async (int? id, IMediator mediator) =>
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