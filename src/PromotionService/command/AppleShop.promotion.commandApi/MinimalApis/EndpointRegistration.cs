using AppleShop.promotion.commandApplication.Commands.Promotion;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppleShop.promotion.commandApi.MinimalApis
{
    public static class EndpointRegistration
    {
        #region Promotion API
        public static IEndpointRouteBuilder PromotionAction(this IEndpointRouteBuilder builder)
        {
            var promotion = builder.MapGroup("/").WithTags("Promotion");
            promotion.MapPost("/create-promotion", async ([FromBody] CreatePromotionCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            promotion.MapPut("/update-promotion/{id}", async (int? id, [FromBody] UpdatePromotionCommand command, IMediator mediator) =>
            {
                command.Id = id;
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            promotion.MapDelete("/delete-promotion/{id}", async (int? id, IMediator mediator) =>
            {
                var command = new DeletePromotionCommand();
                command.Id = id;
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            return builder;
        }
        #endregion
    }
}