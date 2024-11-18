using AppleShop.inventory.Domain.Abstractions.IRepositories;
using AppleShop.Share.Events.Inventory.Command;
using AutoMapper;
using MassTransit;
using Entities = AppleShop.inventory.Domain.Entities;

namespace AppleShop.inventory.commandInfrastructure.Consumers.Inventory
{
    public class InventoryCreateEventConsumer : IConsumer<InventoryCreateEvent>
    {
        private readonly IInventoryRepository iventoryRepository;
        private readonly IMapper mapper;

        public InventoryCreateEventConsumer(IInventoryRepository iventoryRepository, IMapper mapper)
        {
            this.iventoryRepository = iventoryRepository;
            this.mapper = mapper;
        }

        public async Task Consume(ConsumeContext<InventoryCreateEvent> context)
        {
            var message = context.Message;
            var entity = mapper.Map<Entities.Inventory>(message);
            iventoryRepository.Create(entity);
            await iventoryRepository.SaveChangesAsync();
            await context.ConsumeCompleted;
        }
    }
}