using AppleShop.inventory.queryApplication.Queries.Inventory;
using MediatR;

namespace AppleShop.inventory.queryApi.MinimalApis
{
    public static class EndpointRegistration
    {
        public static IEndpointRouteBuilder InventoryAction(this IEndpointRouteBuilder builder)
        {
            var inventory = builder.MapGroup("/").WithTags("Inventory"); ;
            inventory.MapGet("/get-all-inventories", async (IMediator mediator) =>
            {
                var query = new GetAllInventoriesQuery();
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });

            inventory.MapGet("/get-inventory-by-id/{id}", async (int? id, IMediator mediator) =>
            {
                var query = new GetInventoryByIdQuery();
                query.Id = id;
                var result = await mediator.Send(query);
                return Results.Ok(result);
            });

            return builder;
        }
    }
}