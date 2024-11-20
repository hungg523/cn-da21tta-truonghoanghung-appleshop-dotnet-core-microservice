using AppleShop.inventory.queryApplication.Queries.Inventory;
using MediatR;

namespace AppleShop.inventory.queryApi.MinimalApis
{
    public static class EndpointRegistration
    {
        public static IEndpointRouteBuilder InventoryAction(this IEndpointRouteBuilder builder)
        {
            var inventory = builder.MapGroup("/inventory").WithTags("Inventory"); ;
            inventory.MapGet("/get-all", async (IMediator mediator) =>
            {
                var command = new GetAllInventoriesQuery();
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            inventory.MapGet("/get-by-id/{id}", async (int? id, IMediator mediator) =>
            {
                var command = new GetInventoryByIdQuery();
                command.Id = id;
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });

            return builder;
        }
    }
}