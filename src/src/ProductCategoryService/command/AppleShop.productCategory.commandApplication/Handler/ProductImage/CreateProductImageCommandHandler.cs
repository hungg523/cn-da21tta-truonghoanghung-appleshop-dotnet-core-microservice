using AppleShop.productCategory.commandApplication.Commands.ProductImage;
using AppleShop.productCategory.commandApplication.Validator.ProductImage;
using AppleShop.productCategory.Domain.Abstractions.IRepositories;
using AppleShop.Share.Abstractions;
using AppleShop.Share.Enumerations;
using AppleShop.Share.Exceptions;
using AppleShop.Share.Shared;
using AutoMapper;
using MediatR;
using Entities = AppleShop.productCategory.Domain.Entities;

namespace AppleShop.productCategory.commandApplication.Handler.ProductImage
{
    public class CreateProductImageCommandHandler : IRequestHandler<CreateProductImageCommand, Result<object>>
    {
        private readonly IProductImageRepository productImageRepository;
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly IFileService fileUploadService;

        public CreateProductImageCommandHandler(IProductImageRepository productImageRepository, IProductRepository productRepository, IMapper mapper, IFileService fileUploadService)
        {
            this.productImageRepository = productImageRepository;
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.fileUploadService = fileUploadService;
        }

        public async Task<Result<object>> Handle(CreateProductImageCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateProductImageCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            var product = await productRepository.FindByIdAsync(request.ProductId!);
            if (product is null) AppleException.ThrowNotFound(typeof(Entities.Product));

            var productImage = mapper.Map<Entities.ProductImage>(request);
            using var transaction = await productImageRepository.BeginTransactionAsync(cancellationToken);
            try
            {
                if (request.ImageData is not null)
                {
                    var fileName = $"{Guid.NewGuid().ToString().Substring(0, 4)}{fileUploadService.GetFileExtensionFromBase64(request.ImageData)}";
                    var path = await fileUploadService.UploadFile(fileName, request.ImageData, AssetType.PRODUCT_IMG);
                    productImage.Url = path;
                }

                productImageRepository.Create(productImage);
                await productImageRepository.SaveChangesAsync(cancellationToken);
                transaction.Commit();
                return Result<object>.Ok();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}