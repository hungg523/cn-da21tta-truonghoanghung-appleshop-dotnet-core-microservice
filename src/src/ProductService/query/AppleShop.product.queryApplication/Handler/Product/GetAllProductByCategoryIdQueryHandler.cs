using AppleShop.product.Domain.Abstractions.IRepositories;
using AppleShop.product.queryApplication.Queries.DTOs;
using AppleShop.product.queryApplication.Queries.Product;
using AppleShop.product.queryApplication.Validator.Product;
using AppleShop.Share.Events.Category.Request;
using AppleShop.Share.Events.Category.Response;
using AppleShop.Share.Events.Inventory.Response;
using AppleShop.Share.Exceptions;
using AppleShop.Share.Shared;
using MassTransit;
using MediatR;
using Entities = AppleShop.product.Domain.Entities;

namespace AppleShop.product.queryApplication.Handler.Product
{
    public class GetAllProductByCategoryIdQueryHandler : IRequestHandler<GetAllProductByCategoryIdQuery, Result<List<ProductFullDTO>>>
    {
        private readonly IProductRepository productRepository;
        private readonly IProductImageRepository productImageRepository;
        private readonly IRequestClient<GetAllStockEvent> inventoryQueryClient;
        private readonly IColorRepository colorRepository;
        private readonly IProductColorRepository productColorRepository;
        private readonly IRequestClient<GetCategoryActivedByIdEvent> categoryClient;

        public GetAllProductByCategoryIdQueryHandler(IProductRepository productRepository,
                                                     IProductImageRepository productImageRepository,
                                                     IRequestClient<GetAllStockEvent> inventoryQueryClient,
                                                     IColorRepository colorRepository,
                                                     IProductColorRepository productColorRepository,
                                                     IRequestClient<GetCategoryActivedByIdEvent> categoryClient)
        {
            this.productRepository = productRepository;
            this.productImageRepository = productImageRepository;
            this.inventoryQueryClient = inventoryQueryClient;
            this.colorRepository = colorRepository;
            this.productColorRepository = productColorRepository;
            this.categoryClient = categoryClient;
        }

        public async Task<Result<List<ProductFullDTO>>> Handle(GetAllProductByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetAllProductByCategoryIdQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            var categoryRequest = await categoryClient.GetResponse<CategoryResponse>(new GetCategoryActivedByIdEvent { Id = request.CategoryId });
            var category = categoryRequest.Message;
            if (category is null) AppleException.ThrowNotFound(typeof(Entities.Product));

            var products = productRepository.FindAll(x => x.CategoryId == request.CategoryId && x.IsActived == 1).ToList();
            var productIds = products.Select(p => p.Id).ToList();

            var inventoryTask = inventoryQueryClient.GetResponse<InventoriesResponse>(new GetAllStockEvent { ProductIds = productIds }, cancellationToken);
            var productImagesTask = productImageRepository.FindAll(x => productIds.Contains(x.ProductId)).ToList();
            var productColorsTask = productColorRepository.FindAll(x => productIds.Contains(x.ProductId)).ToList();
            var colorsTask = colorRepository.FindAll().ToList();

            var inventoryDict = inventoryTask.Result.Message.Inventories.ToDictionary(i => i.ProductId, i => i.Stock);
            var imagesDict = productImagesTask.GroupBy(img => img.ProductId).ToDictionary(g => g.Key, g => g.ToList());
            var productColorsDict = productColorsTask.GroupBy(pc => pc.ProductId).ToDictionary(g => g.Key, g => g.Select(pc => pc.ColorId).ToList());
            var colorsDict = colorsTask.ToDictionary(c => c.Id);

            var productDtos = products.Select(product => new ProductFullDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                DiscountPrice = product.DiscountPrice,
                Stock = inventoryDict.ContainsKey(product.Id) ? inventoryDict[product.Id] : 0,
                IsActived = product.IsActived,
                Images = imagesDict.TryGetValue(product.Id, out var images) ? images.Select(image =>
                    new ProductImageDTO
                    {
                        Id = image.Id,
                        Title = image.Title,
                        Url = image.Url,
                        Position = image.Position,
                    }).ToList() : new List<ProductImageDTO>(),
                Colors = productColorsDict.TryGetValue(product.Id, out var colorIds) ? colorIds.Select(colorId => colorsDict[colorId])
                    .Select(color => new ColorDTO { Id = color.Id, Name = color.Name }).ToList() : new List<ColorDTO>()
            }).ToList();
            return Result<List<ProductFullDTO>>.Ok(productDtos);
        }
    }
}