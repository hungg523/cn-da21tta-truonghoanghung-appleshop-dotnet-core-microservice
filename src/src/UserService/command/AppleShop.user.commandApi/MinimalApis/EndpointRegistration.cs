using AppleShop.user.commandApplication.Commands.User;
using AppleShop.user.commandApplication.Commands.UserAddress;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppleShop.user.commandApi.MinimalApis
{
    public static class EndpointRegistration
    {
        #region User API
        public static IEndpointRouteBuilder UserAction(this IEndpointRouteBuilder builder)
        {
            var user = builder.MapGroup("/").WithTags("User");
            user.MapPut("/update-profile-user/{id}", async (int? id, [FromBody] UpdateProfileUserCommand command, IMediator mediator) =>
            {
                command.Id = id;
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            return builder;
        }
        #endregion

        #region User Address API
        public static IEndpointRouteBuilder UserAddressAction(this IEndpointRouteBuilder builder)
        {
            var userAddress = builder.MapGroup("/").WithTags("UserAddress");
            userAddress.MapPost("/create-user-address", async ([FromBody] CreateUserAddressCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            userAddress.MapPut("/update-user-address/{id}", async (int? id, [FromBody] UpdateUserAddressCommand command, IMediator mediator) =>
            {
                command.Id = id;
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            userAddress.MapDelete("/delete-user-address/{id}", async (int? id, IMediator mediator) =>
            {
                var command = new DeleteUserAddressCommand();
                command.Id = id;
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            return builder;
        }
        #endregion
    }
}