using AppleShop.order.commandApplication.Commands.Order;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppleShop.order.commandApi.MinimalApis
{
    public static class EndpointRegistration
    {
        #region Order API
        public static IEndpointRouteBuilder CartAction(this IEndpointRouteBuilder builder)
        {
            var order = builder.MapGroup("/").WithTags("Order");
            order.MapPost("/create-order", async ([FromBody] CreateOrderCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            order.MapPut("/change-status/{id}", async (int? id, [FromBody] ChangeOrderStatusCommand command, IMediator mediator) =>
            {
                command.OrderId = id;
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            order.MapPut("/cancel-order/{orderId}/{userId}", async (int? orderId, int ? userId, IMediator mediator) =>
            {
                var command = new CancelOrderCommand();
                command.OrderId = orderId;
                command.UserId = userId;
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            order.MapPut("/success-order/{id}", async (int? id, IMediator mediator) =>
            {
                var command = new SuccessOrderCommand();
                command.OrderId = id;
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            return builder;
        }
        #endregion
    }
}