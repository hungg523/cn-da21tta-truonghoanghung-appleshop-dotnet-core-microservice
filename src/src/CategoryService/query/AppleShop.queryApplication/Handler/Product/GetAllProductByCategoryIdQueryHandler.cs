using Entities = AppleShop.Domain.Entities;
using AppleShop.queryApplication.Queries.Product;
using AppleShop.Share.Shared;
using MediatR;
using AppleShop.queryApplication.Validator.Product;
using AppleShop.Share.Exceptions;
using AppleShop.Domain.Abstractions.IRepositories;
using AppleShop.queryApplication.Queries.DTOs;
using AutoMapper;
using AppleShop.Domain.Entities;

namespace AppleShop.queryApplication.Handler.Product
{
    public class GetAllProductByCategoryIdQueryHandler : IRequestHandler<GetAllProductByCategoryIdQuery, Result<List<ProductFullDTO>>>
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IProductImageRepository productImageRepository;
        private readonly IMapper mapper;

        public GetAllProductByCategoryIdQueryHandler(IProductRepository productRepository, ICategoryRepository categoryRepository, IProductImageRepository productImageRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.productImageRepository = productImageRepository;
            this.mapper = mapper;
        }

        public async Task<Result<List<ProductFullDTO>>> Handle(GetAllProductByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetAllProductByCategoryIdQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            var category = await categoryRepository.FindByIdAsync(request.CategoryId);
            if (category is null) AppleException.ThrowNotFound(typeof(Entities.Product));

            var products = productRepository.FindAll(x => x.CategoryId == request.CategoryId).ToList();
            var productDtos = products.Select(product =>
            {
                var productDto = new ProductFullDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    StockQuantity = product.StockQuantity,
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