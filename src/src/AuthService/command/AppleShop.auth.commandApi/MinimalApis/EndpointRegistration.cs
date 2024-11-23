using AppleShop.auth.commandApplication.Commands.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppleShop.auth.commandApi.MinimalApis
{
    public static class EndpointRegistration
    {
        #region Auth API
        public static IEndpointRouteBuilder AuthAction(this IEndpointRouteBuilder builder)
        {
            var auth = builder.MapGroup("/auth").WithTags("Auth");
            auth.MapPost("/register", async ([FromBody] RegisterCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            auth.MapPut("/vertify-otp", async (string? email, [FromBody] VertifyOTPCommand command, IMediator mediator) =>
            {
                command.Email = email;
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            auth.MapPost("/resend-otp", async (string? email, IMediator mediator) =>
            {
                var command = new ResendOTPCommand();
                command.Email = email;
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            auth.MapPost("/login", async (string? email, [FromBody] LoginCommand command,IMediator mediator) =>
            {
                command.Email = email;
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            return builder;
        }
        #endregion
    }
}