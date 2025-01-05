using AppleShop.product.Domain.Abstractions.IRepositories;
using AppleShop.product.queryApplication.Queries.DTOs;
using AppleShop.product.queryApplication.Queries.Product;
using AppleShop.product.queryApplication.Validator.Product;
using AppleShop.Share.Events.Inventory.Response;
using AppleShop.Share.Exceptions;
using AppleShop.Share.Shared;
using AutoMapper;
using MassTransit;
using MediatR;
using Entities = AppleShop.product.Domain.Entities;

namespace AppleShop.product.queryApplication.Handler.Product
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductFullDTO>>
    {
        private readonly IProductRepository productRepository;
        private readonly IProductImageRepository productImageRepository;
        private readonly IMapper mapper;
        private readonly IRequestClient<GetStockByProductIdEvent> inventoryQueryClient;
        private readonly IColorRepository colorRepository;
        private readonly IProductColorRepository productColorRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository,
                                          IProductImageRepository productImageRepository,
                                          IMapper mapper,
                                          IRequestClient<GetStockByProductIdEvent> inventoryQueryClient,
                                          IColorRepository colorRepository,
                                          IProductColorRepository productColorRepository)
        {
            this.productRepository = productRepository;
            this.productImageRepository = productImageRepository;
            this.mapper = mapper;
            this.inventoryQueryClient = inventoryQueryClient;
            this.colorRepository = colorRepository;
            this.productColorRepository = productColorRepository;
        }

        public async Task<Result<ProductFullDTO>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetProductByIdQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            var product = await productRepository.FindByIdAsync(request.Id);
            if (product is null) AppleException.ThrowNotFound(typeof(Entities.Product));
            var inventoryQuery = new GetStockByProductIdEvent { ProductId = product.Id };
            var inventoryResponse = await inventoryQueryClient.GetResponse<InventoryResponse>(inventoryQuery, cancellationToken);
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

            var productColor = productColorRepository.FindAll(x => x.ProductId == product.Id).Select(x => x.ColorId).ToList();
            var colors = colorRepository.FindAll(x => productColor.Contains(x.Id)).ToList();
            productDto.Colors = colors.Select(color => new ColorDTO
            {
                Id = color.Id,
                Name = color.Name
            }).ToList();

            return Result<ProductFullDTO>.Ok(productDto);
        }
    }
}