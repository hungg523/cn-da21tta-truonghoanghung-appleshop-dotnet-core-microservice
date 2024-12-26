using AppleShop.product.Domain.Abstractions.IRepositories;
using AppleShop.Share.Events.Order.Request;
using MassTransit;

namespace AppleShop.product.commandInfrastructure.Consumers.Product
{
    public class UpdateSoldQuantityEventConsumer : IConsumer<UpdateSoldQuantityEvent>
    {
        private readonly IProductRepository productRepository;

        public UpdateSoldQuantityEventConsumer(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task Consume(ConsumeContext<UpdateSoldQuantityEvent> context)
        {
            var message = context.Message;
            var productIds = message.SoldQuantity.Keys.ToList();
            var products = productRepository.FindAll(x => productIds.Contains(x.Id.Value), true).ToList();

            foreach (var product in products)
            {
                if (message.SoldQuantity.TryGetValue(product.Id.Value, out int soldQuantity))
                {
                    product.SoldQuantity = soldQuantity;
                }
            }
            await productRepository.SaveChangesAsync();
        }
    }
}