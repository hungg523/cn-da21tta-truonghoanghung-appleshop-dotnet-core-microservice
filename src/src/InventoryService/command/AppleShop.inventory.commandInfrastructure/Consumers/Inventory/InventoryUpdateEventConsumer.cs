using AppleShop.inventory.Domain.Abstractions.IRepositories;
using AppleShop.Share.Events.Inventory.Command;
using AppleShop.Share.Exceptions;
using AutoMapper;
using MassTransit;
using Entities = AppleShop.inventory.Domain.Entities;

namespace AppleShop.inventory.commandInfrastructure.Consumers.Inventory
{
    public class InventoryUpdateEventConsumer : IConsumer<InventoryUpdateEvent>
    {
        private readonly IInventoryRepository iventoryRepository;
        private readonly IMapper mapper;

        public InventoryUpdateEventConsumer(IInventoryRepository iventoryRepository, IMapper mapper)
        {
            this.iventoryRepository = iventoryRepository;
            this.mapper = mapper;
        }

        public async Task Consume(ConsumeContext<InventoryUpdateEvent> context)
        {
            var message = context.Message;
            var entity = await iventoryRepository.FindSingleAsync(x => x.ProductId == message.ProductId!.Value, true);
            if (entity is null) AppleException.ThrowNotFound(typeof(Entities.Inventory));

            mapper.Map(message, entity);
            iventoryRepository.Update(entity!);
            await iventoryRepository.SaveChangesAsync();
            await context.ConsumeCompleted;
        }
    }
}