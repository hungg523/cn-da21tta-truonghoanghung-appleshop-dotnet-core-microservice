using AppleShop.Domain.Abstractions.IRepositories;
using AppleShop.Domain.Entities;
using AppleShop.queryApplication.Queries.DTOs;
using AppleShop.queryApplication.Queries.Product;
using AppleShop.Share.Shared;
using AutoMapper;
using MediatR;

namespace AppleShop.queryApplication.Handler.Product
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, Result<List<ProductFullDTO>>>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly IProductImageRepository productImageRepository;

        public GetAllProductQueryHandler(IProductRepository productRepository, IProductImageRepository productImageRepository)
        {
            this.productRepository = productRepository;
            this.productImageRepository = productImageRepository;
        }

        public async Task<Result<List<ProductFullDTO>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var products = productRepository.FindAll().ToList();
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