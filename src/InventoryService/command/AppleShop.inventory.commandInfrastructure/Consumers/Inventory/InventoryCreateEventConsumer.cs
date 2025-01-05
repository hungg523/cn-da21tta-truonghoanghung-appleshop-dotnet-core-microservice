using AppleShop.inventory.Domain.Abstractions.IRepositories;
using AppleShop.Share.Events.Inventory.Request;
using AutoMapper;
using MassTransit;
using Entities = AppleShop.inventory.Domain.Entities;

namespace AppleShop.inventory.commandInfrastructure.Consumers.Inventory
{
    public class InventoryCreateEventConsumer : IConsumer<InventoryCreateEvent>
    {
        private readonly IInventoryRepository inventoryRepository;
        private readonly IMapper mapper;

        public InventoryCreateEventConsumer(IInventoryRepository inventoryRepository, IMapper mapper)
        {
            this.inventoryRepository = inventoryRepository;
            this.mapper = mapper;
        }

        public async Task Consume(ConsumeContext<InventoryCreateEvent> context)
        {
            var message = context.Message;
            var inventory = mapper.Map<Entities.Inventory>(message);
            inventoryRepository.Create(inventory);
            await inventoryRepository.SaveChangesAsync();
            await context.ConsumeCompleted;
        }
    }
}