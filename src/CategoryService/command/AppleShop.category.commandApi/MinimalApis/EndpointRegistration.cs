using AppleShop.category.commandApplication.Commands.Category;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppleShop.category.commandApi.MinimalApis
{
    public static class EndpointRegistration
    {
        #region Category API
        public static IEndpointRouteBuilder CategoryAction(this IEndpointRouteBuilder builder)
        {
            var category = builder.MapGroup("/").WithTags("Category");
            category.MapPost("/create-category", async ([FromBody]CreateCategoryCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            category.MapPut("/update-category/{id}", async (int? id, [FromBody]UpdateCategoryCommand command, IMediator mediator) =>
            {
                command.Id = id;
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            return builder;
        }
        #endregion
    }
}