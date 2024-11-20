using AppleShop.productCategory.Domain.Abstractions.IRepositories;
using AppleShop.Share.Events.Cart.Query;
using MassTransit;

namespace AppleShop.productCategory.queryInfrastructure.Consumers.Product
{
    public class GetProductByIdEventConsumer : IConsumer<GetProductByIdEvent>
    {
        private readonly IProductRepository productRepository;

        public GetProductByIdEventConsumer(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task Consume(ConsumeContext<GetProductByIdEvent> context)
        {
            var message = context.Message;
            var product = await productRepository.FindByIdAsync(message.ProductId);
            if (product is null)
            {
                await context.RespondAsync(new ProductResponse { ProductId = null });
                return;
            }
            var response = new ProductResponse
            {
                ProductId = product.Id,
                Price = product.Price,
                DiscountPrice = product.DiscountPrice,
            };

            await context.RespondAsync(response);
        }
    }
}