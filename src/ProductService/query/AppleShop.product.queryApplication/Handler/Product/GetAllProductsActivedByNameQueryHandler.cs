﻿using AppleShop.product.Domain.Abstractions.IRepositories;
using AppleShop.product.queryApplication.Queries.DTOs;
using AppleShop.product.queryApplication.Queries.Product;
using AppleShop.Share.Abstractions;
using AppleShop.Share.Events.Category.Request;
using AppleShop.Share.Events.Category.Response;
using AppleShop.Share.Events.Inventory.Response;
using AppleShop.Share.Shared;
using MassTransit;
using MediatR;

namespace AppleShop.product.queryApplication.Handler.Product
{
    public class GetAllProductsActivedByNameQueryHandler : IRequestHandler<GetAllProductsActivedByNameQuery, Result<List<ProductFullDTO>>>
    {
        private readonly IProductRepository productRepository;
        private readonly IProductImageRepository productImageRepository;
        private readonly IShareEventDispatcher shareEventDispatcher;
        private readonly IRequestClient<GetAllStockEvent> inventoryQueryClient;
        private readonly IColorRepository colorRepository;
        private readonly IProductColorRepository productColorRepository;
        private readonly IRequestClient<GetAllCategoryActivedEvent> categoryClient;

        public GetAllProductsActivedByNameQueryHandler(IProductRepository productRepository,
                                                       IProductImageRepository productImageRepository,
                                                       IShareEventDispatcher shareEventDispatcher,
                                                       IRequestClient<GetAllStockEvent> inventoryQueryClient,
                                                       IProductColorRepository productColorRepository,
                                                       IColorRepository colorRepository,
                                                       IRequestClient<GetAllCategoryActivedEvent> categoryClient)
        {
            this.productRepository = productRepository;
            this.productImageRepository = productImageRepository;
            this.shareEventDispatcher = shareEventDispatcher;
            this.inventoryQueryClient = inventoryQueryClient;
            this.productColorRepository = productColorRepository;
            this.colorRepository = colorRepository;
            this.categoryClient = categoryClient;
        }

        public async Task<Result<List<ProductFullDTO>>> Handle(GetAllProductsActivedByNameQuery request, CancellationToken cancellationToken)
        {
            var categoryRequest = await categoryClient.GetResponse<CategoriesResponse>(new GetAllCategoryActivedEvent(), cancellationToken);
            var category = categoryRequest.Message;

            var products = productRepository.FindAll(x => x.Name.ToLower().Contains(request.Name.ToLower()) && x.IsActived == 1 && category.Ids.Contains(x.CategoryId)).ToList();
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