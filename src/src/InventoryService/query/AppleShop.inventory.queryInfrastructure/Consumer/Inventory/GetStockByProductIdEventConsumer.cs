using AppleShop.inventory.Domain.Abstractions.IRepositories;
using AppleShop.Share.Events.Inventory.Query;
using MassTransit;

namespace AppleShop.inventory.queryInfrastructure.Consumer.Inventory
{
    public class GetStockByProductIdEventConsumer : IConsumer<GetStockByProductIdEvent>
    {
        private readonly IInventoryRepository inventoryRepository;

        public GetStockByProductIdEventConsumer(IInventoryRepository inventoryRepository)
        {
            this.inventoryRepository = inventoryRepository;
        }

        public async Task Consume(ConsumeContext<GetStockByProductIdEvent> context)
        {
            var message = context.Message;
            var inventory = await inventoryRepository.FindSingleAsync(x => x.ProductId == message.ProductId);
            var inventoryResponse = new InventoryResponse
            {
                ProductId = inventory.ProductId,
                Stock = inventory.Stock
            };

            await context.RespondAsync(inventoryResponse);
        }
    }
}