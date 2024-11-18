using AppleShop.productCategory.Domain.Abstractions.IRepositories;
using AppleShop.productCategory.queryApplication.Queries.DTOs;
using AppleShop.productCategory.queryApplication.Queries.Product;
using AppleShop.productCategory.queryApplication.Validator.Product;
using AppleShop.Share.Events.Inventory.Query;
using AppleShop.Share.Exceptions;
using AppleShop.Share.Shared;
using AutoMapper;
using MassTransit;
using MediatR;
using Entities = AppleShop.productCategory.Domain.Entities;

namespace AppleShop.productCategory.queryApplication.Handler.Product
{
    public class GetAllProductByCategoryIdQueryHandler : IRequestHandler<GetAllProductByCategoryIdQuery, Result<List<ProductFullDTO>>>
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IProductImageRepository productImageRepository;
        private readonly IRequestClient<GetAllStockEvent> inventoryQueryClient;

        public GetAllProductByCategoryIdQueryHandler(IProductRepository productRepository, ICategoryRepository categoryRepository, IProductImageRepository productImageRepository, IRequestClient<GetAllStockEvent> inventoryQueryClient)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.productImageRepository = productImageRepository;
            this.inventoryQueryClient = inventoryQueryClient;
        }

        public async Task<Result<List<ProductFullDTO>>> Handle(GetAllProductByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetAllProductByCategoryIdQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            var category = await categoryRepository.FindByIdAsync(request.CategoryId);
            if (category is null) AppleException.ThrowNotFound(typeof(Entities.Product));

            var products = productRepository.FindAll(x => x.CategoryId == request.CategoryId).ToList();
            var inventoryQuery = new GetAllStockEvent { ProductIds = products.Select(p => p.Id).ToList() };
            var inventoryResponse = await inventoryQueryClient.GetResponse<InventoryResponse>(inventoryQuery, cancellationToken);
            var inventoryDict = inventoryResponse.Message.Inventories.ToDictionary(i => i.ProductId, i => i.Stock);
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