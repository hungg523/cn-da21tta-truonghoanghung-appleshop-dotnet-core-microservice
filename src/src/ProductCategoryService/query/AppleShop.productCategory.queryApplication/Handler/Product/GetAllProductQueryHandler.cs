using AppleShop.productCategory.Domain.Abstractions.IRepositories;
using AppleShop.productCategory.queryApplication.Queries.DTOs;
using AppleShop.productCategory.queryApplication.Queries.Product;
using AppleShop.Share.Abstractions;
using AppleShop.Share.Events.Color.Query;
using AppleShop.Share.Events.Inventory.Query;
using AppleShop.Share.Shared;
using MassTransit;
using MediatR;

namespace AppleShop.productCategory.queryApplication.Handler.Product
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, Result<List<ProductFullDTO>>>
    {
        private readonly IProductRepository productRepository;
        private readonly IProductImageRepository productImageRepository;
        private readonly IShareEventDispatcher shareEventDispatcher;
        private readonly IRequestClient<GetAllStockEvent> inventoryQueryClient;
        private readonly IRequestClient<GetAllColorNameEvent> colorQueryClient;

        public GetAllProductQueryHandler(IProductRepository productRepository, IProductImageRepository productImageRepository, IShareEventDispatcher shareEventDispatcher, IRequestClient<GetAllStockEvent> inventoryQueryClient, IRequestClient<GetAllColorNameEvent> colorQueryClient)
        {
            this.productRepository = productRepository;
            this.productImageRepository = productImageRepository;
            this.shareEventDispatcher = shareEventDispatcher;
            this.inventoryQueryClient = inventoryQueryClient;
            this.colorQueryClient = colorQueryClient;
        }

        public async Task<Result<List<ProductFullDTO>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var products = productRepository.FindAll().ToList();
            var productIds = products.Select(p => p.Id).ToList();

            var inventoryQuery = new GetAllStockEvent { ProductIds = productIds };
            var inventoryResponse = await inventoryQueryClient.GetResponse<InventoryResponse>(inventoryQuery, cancellationToken);
            var inventoryDict = inventoryResponse.Message.Inventories.ToDictionary(i => i.ProductId, i => i.Stock);

            var colorQuery = new GetAllColorNameEvent { ProductIds = productIds };
            var colorResponse = await colorQueryClient.GetResponse<ColorResponse>(colorQuery, cancellationToken);
            var colorDict = colorResponse.Message.Colors.GroupBy(c => c.ProductId).ToDictionary(g => g.Key, g => g.Select(c => c.Color).ToList());

            var productDtos = products.Select(product =>
            {
                var productDto = new ProductFullDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    DiscountPrice = product.DiscountPrice,
                    Stock = inventoryDict.ContainsKey(product.Id) ? inventoryDict[product.Id] : 0,
                    IsActived = product.IsActived,
                    Colors = colorDict.ContainsKey(product.Id) ? colorDict[product.Id] : new List<string>()
                };

                var productImages = productImageRepository.FindAll(x => x.ProductId == product.Id).ToList();
                productDto.Images = productImages.Select(image => new ProductImageDTO
                {
                    Id = image.Id,
                    Title = image.Title,
                    Url = image.Url,
                    Position = image.Position,
                }).ToList();

                return productDto;
            }).ToList();

            return Result<List<ProductFullDTO>>.Ok(productDtos);
        }

    }
}