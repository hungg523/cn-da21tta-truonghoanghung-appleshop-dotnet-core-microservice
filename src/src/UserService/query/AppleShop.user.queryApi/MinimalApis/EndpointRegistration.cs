using MediatR;

namespace AppleShop.user.queryApi.MinimalApis
{
    public static class EndpointRegistration
    {
        #region Order API
        public static IEndpointRouteBuilder UserAction(this IEndpointRouteBuilder builder)
        {
            var order = builder.MapGroup("/order").WithTags("Order"); ;

            return builder;
        }
        #endregion
    }
}