using AppleShop.product.Domain.Abstractions.IRepositories;
using AppleShop.Share.Events.Cart.Response;
using MassTransit;

namespace AppleShop.product.queryInfrastructure.Consumers.Product
{
    public class GetAllCartItemByUserIdEventConsumer : IConsumer<GetAllCartItemByUserIdEvent>
    {
        private readonly IProductRepository productRepository;

        public GetAllCartItemByUserIdEventConsumer(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task Consume(ConsumeContext<GetAllCartItemByUserIdEvent> context)
        {
            var message = context.Message;
            var products = productRepository.FindAll(x => message.ProductIds.Contains(x.Id)).ToList();
            var productsResponse = new ProductsResponse
            {
                Products = products.Select(p => new ProductResponse
                {
                    ProductId = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    DiscountPrice = p.DiscountPrice,
                }).ToList()
            };

            await context.RespondAsync(productsResponse);
        }
    }
}