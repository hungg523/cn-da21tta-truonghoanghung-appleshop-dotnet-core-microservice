﻿using AppleShop.productCategory.queryApplication.Queries.Category;
using AppleShop.productCategory.queryApplication.Queries.Product;
using MediatR;

namespace AppleShop.productCategory.queryApi.MinimalApis
{
    public static class EndpointRegistration
    {
        #region Category API
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
        #endregion

        #region Product API
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
        #endregion
    }
}