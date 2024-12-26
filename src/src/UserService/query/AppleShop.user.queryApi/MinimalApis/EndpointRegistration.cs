using AppleShop.user.queryApplication.Queries.User;
using AppleShop.user.queryApplication.Queries.UserAddress;
using MediatR;

namespace AppleShop.user.queryApi.MinimalApis
{
    public static class EndpointRegistration
    {
        #region User API
        public static IEndpointRouteBuilder UserAction(this IEndpointRouteBuilder builder)
        {
            var user = builder.MapGroup("/").WithTags("User"); ;
            user.MapGet("/get-profile-user-by-id/{id}", async (int? id, IMediator mediator) =>
            {
                var query = new GetProfileUserByIdQuery();
                query.Id = id;
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });

            return builder;
        }
        #endregion

        #region User Address API
        public static IEndpointRouteBuilder UserAddressAction(this IEndpointRouteBuilder builder)
        {
            var userAddress = builder.MapGroup("/").WithTags("User Address"); ;
            userAddress.MapGet("/get-all-address-by-user-id/{id}", async (int? id, IMediator mediator) =>
            {
                var query = new GetAllAddressByUserIdQuery();
                query.UserId = id;
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });

            userAddress.MapGet("/get-address-by-id/{id}", async (int? id, IMediator mediator) =>
            {
                var query = new GetUserAddressByIdQuery();
                query.Id = id;
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });

            return builder;
        }
        #endregion
    }
}