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
            var auth = builder.MapGroup("/").WithTags("Auth");
            auth.MapPost("/register", async ([FromBody] RegisterCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            auth.MapPut("/vertify-otp", async ([FromBody] VertifyOTPCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            auth.MapPost("/resend-otp", async ([FromBody] ResendOTPCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            auth.MapPost("/login", async ([FromBody] LoginCommand command,IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            auth.MapPost("/refresh-token", async ([FromBody] RefreshTokenCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            auth.MapPost("/logout", async ([FromBody] LogoutCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            auth.MapPost("/reset-password", async ([FromBody] ResetPasswordCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            auth.MapPut("/change-password", async (string? email, [FromBody] ChangePasswordCommand command, IMediator mediator) =>
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