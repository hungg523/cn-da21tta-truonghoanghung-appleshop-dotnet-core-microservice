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
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductFullDTO>>
    {
        private readonly IProductRepository productRepository;
        private readonly IProductImageRepository productImageRepository;
        private readonly IMapper mapper;
        private readonly IRequestClient<GetStockByProductIdEvent> inventoryQueryClient;

        public GetProductByIdQueryHandler(IProductRepository productRepository, IProductImageRepository productImageRepository, IMapper mapper, IRequestClient<GetStockByProductIdEvent> inventoryQueryClient)
        {
            this.productRepository = productRepository;
            this.productImageRepository = productImageRepository;
            this.mapper = mapper;
            this.inventoryQueryClient = inventoryQueryClient;
        }

        public async Task<Result<ProductFullDTO>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetProductByIdQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            var product = await productRepository.FindByIdAsync(request.Id);
            if (product is null) AppleException.ThrowNotFound(typeof(Entities.Product));
            var inventoryQuery = new GetStockByProductIdEvent { ProductId = product.Id };
            var inventoryResponse = await inventoryQueryClient.GetResponse<InventoryInfo>(inventoryQuery, cancellationToken);
            var productDto = new ProductFullDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                DiscountPrice = product.DiscountPrice,
                Stock = inventoryResponse?.Message?.Stock ?? 0,
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
            return Result<ProductFullDTO>.Ok(productDto);
        }
    }
}