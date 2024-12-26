using AppleShop.inventory.Domain.Abstractions.IRepositories;
using AppleShop.Share.Events.Inventory.Request;
using AppleShop.Share.Exceptions;
using AutoMapper;
using MassTransit;
using Entities = AppleShop.inventory.Domain.Entities;

namespace AppleShop.inventory.commandInfrastructure.Consumers.Inventory
{
    public class InventoryUpdateEventConsumer : IConsumer<InventoryUpdateEvent>
    {
        private readonly IInventoryRepository inventoryRepository;
        private readonly IMapper mapper;

        public InventoryUpdateEventConsumer(IInventoryRepository inventoryRepository, IMapper mapper)
        {
            this.inventoryRepository = inventoryRepository;
            this.mapper = mapper;
        }

        public async Task Consume(ConsumeContext<InventoryUpdateEvent> context)
        {
            var message = context.Message;
            var inventory = await inventoryRepository.FindSingleAsync(x => x.ProductId == message.ProductId!.Value, true);
            if (inventory is null) AppleException.ThrowNotFound(typeof(Entities.Inventory));

            mapper.Map(message, inventory);
            inventoryRepository.Update(inventory!);
            await inventoryRepository.SaveChangesAsync();
            await context.ConsumeCompleted;
        }
    }
}