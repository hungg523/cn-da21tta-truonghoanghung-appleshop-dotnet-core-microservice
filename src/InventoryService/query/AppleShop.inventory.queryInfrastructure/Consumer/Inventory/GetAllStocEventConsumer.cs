using AppleShop.inventory.Domain.Abstractions.IRepositories;
using AppleShop.Share.Events.Inventory.Response;
using MassTransit;

namespace AppleShop.inventory.queryInfrastructure.Consumer.Inventory
{
    public class GetAllStocEventConsumer : IConsumer<GetAllStockEvent>
    {
        private readonly IInventoryRepository inventoryRepository;

        public GetAllStocEventConsumer(IInventoryRepository inventoryRepository)
        {
            this.inventoryRepository = inventoryRepository;
        }

        public async Task Consume(ConsumeContext<GetAllStockEvent> context)
        {
            var message = context.Message;
            var inventories = inventoryRepository.FindAll(x => message.ProductIds.Contains(x.ProductId));
            var inventoryResponse = new InventoriesResponse
            {
                Inventories = inventories.Select(i => new InventoryResponse
                {
                    ProductId = i.ProductId,
                    Stock = i.Stock
                }).ToList()
            };

            await context.RespondAsync(inventoryResponse);
        }
    }
}