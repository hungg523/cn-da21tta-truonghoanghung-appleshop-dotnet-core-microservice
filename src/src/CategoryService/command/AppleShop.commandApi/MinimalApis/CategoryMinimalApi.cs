using AppleShop.commandApplication.Commands.Category;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppleShop.commandApi.MinimalApis
{
    public static class CategoryMinimalApi
    {
        public static IEndpointRouteBuilder CategoryAction(this IEndpointRouteBuilder builder)
        {
            var category = builder.MapGroup("/category");
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
    }
}