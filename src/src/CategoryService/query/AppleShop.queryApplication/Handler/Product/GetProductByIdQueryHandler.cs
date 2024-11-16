using AppleShop.Domain.Abstractions.IRepositories;
using AppleShop.Domain.Entities;
using AppleShop.queryApplication.Queries.DTOs;
using AppleShop.queryApplication.Queries.Product;
using AppleShop.queryApplication.Validator.Product;
using AppleShop.Share.Exceptions;
using AppleShop.Share.Shared;
using AutoMapper;
using MediatR;
using Entities = AppleShop.Domain.Entities;

namespace AppleShop.queryApplication.Handler.Product
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductFullDTO>>
    {
        private readonly IProductRepository productRepository;
        private readonly IProductImageRepository productImageRepository;
        private readonly IMapper mapper;

        public GetProductByIdQueryHandler(IProductRepository productRepository, IProductImageRepository productImageRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.productImageRepository = productImageRepository;
            this.mapper = mapper;
        }

        public async Task<Result<ProductFullDTO>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetProductByIdQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            var product = await productRepository.FindByIdAsync(request.Id);
            if (product is null) AppleException.ThrowNotFound(typeof(Entities.Product));

            var productDto = mapper.Map<ProductFullDTO>(product);

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