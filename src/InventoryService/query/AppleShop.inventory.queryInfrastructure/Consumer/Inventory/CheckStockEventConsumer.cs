using AppleShop.inventory.Domain.Abstractions.IRepositories;
using AppleShop.Share.Events.Inventory.Request;
using AppleShop.Share.Events.Inventory.Response;
using MassTransit;

namespace AppleShop.inventory.queryInfrastructure.Consumer.Inventory
{
    public class CheckStockEventConsumer : IConsumer<CheckStockEvent>
    {
        private readonly IInventoryRepository inventoryRepository;

        public CheckStockEventConsumer(IInventoryRepository inventoryRepository)
        {
            this.inventoryRepository = inventoryRepository;
        }

        public async Task Consume(ConsumeContext<CheckStockEvent> context)
        {
            var message = context.Message;
            var inventory = await inventoryRepository.FindSingleAsync(x => x.ProductId == message.ProductId, true);
            if (inventory is null || message.Stock > inventory.Stock)
            {
                await context.RespondAsync(new CheckStockResponse { Success = false });
                return;
            }

            inventory.Stock -= message.Stock;
            inventoryRepository.Update(inventory);
            await inventoryRepository.SaveChangesAsync();
            await context.RespondAsync(new CheckStockResponse { Success = true });
        }
    }
}