using AppleShop.order.commandApplication.Commands.Order;
using AppleShop.order.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppleShop.order.commandApi.MinimalApis
{
    public static class EndpointRegistration
    {
        #region Order API
        public static IEndpointRouteBuilder CartAction(this IEndpointRouteBuilder builder)
        {
            var order = builder.MapGroup("/order").WithTags("Order");
            order.MapPost("/create", async ([FromBody] CreateOrderCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            order.MapPut("/change-status/{orderId}", async (int? orderId, [FromBody] ChangeOrderStatusCommand command, IMediator mediator) =>
            {
                command.OrderId = orderId;
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            order.MapPut("/cancel/{orderId}/{userId}", async (int? orderId, int ? userId, IMediator mediator) =>
            {
                var command = new CancelOrderCommand();
                command.OrderId = orderId;
                command.UserId = userId;
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            return builder;
        }
        #endregion
    }
}