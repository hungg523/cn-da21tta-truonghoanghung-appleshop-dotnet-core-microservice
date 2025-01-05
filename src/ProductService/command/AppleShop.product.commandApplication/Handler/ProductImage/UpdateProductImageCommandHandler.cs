using AppleShop.product.commandApplication.Commands.ProductImage;
using AppleShop.product.commandApplication.Validator.ProductImage;
using AppleShop.product.Domain.Abstractions.IRepositories;
using AppleShop.Share.Abstractions;
using AppleShop.Share.Enumerations;
using AppleShop.Share.Exceptions;
using AppleShop.Share.Shared;
using AutoMapper;
using MediatR;
using Entities = AppleShop.product.Domain.Entities;

namespace AppleShop.product.commandApplication.Handler.ProductImage
{
    public class UpdateProductImageCommandHandler : IRequestHandler<UpdateProductImageCommand, Result<object>>
    {
        private readonly IProductImageRepository productImageRepository;
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly IFileService fileUploadService;

        public UpdateProductImageCommandHandler(IProductImageRepository productImageRepository, IProductRepository productRepository, IMapper mapper, IFileService fileUploadService)
        {
            this.productImageRepository = productImageRepository;
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.fileUploadService = fileUploadService;
        }

        public async Task<Result<object>> Handle(UpdateProductImageCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateProductImageCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            using var transaction = await productImageRepository.BeginTransactionAsync(cancellationToken);
            try
            {
                var productImage = await productImageRepository.FindByIdAsync(request.Id, true);
                if (productImage is null) AppleException.ThrowNotFound(typeof(Entities.ProductImage));

                var product = await productRepository.FindByIdAsync(request.ProductId, true);
                if (product is null) AppleException.ThrowNotFound(typeof(Entities.Product));

                if (request.ImageData is not null)
                {
                    string fileName = (Path.GetFileName(productImage.Url) is { } name &&
                            Path.GetExtension(name)?.ToLowerInvariant() == fileUploadService.GetFileExtensionFromBase64(request.ImageData)?.ToLowerInvariant()) ? name : $"{Guid.NewGuid().ToString().Substring(0, 4)}{fileUploadService.GetFileExtensionFromBase64(request.ImageData)}";
                    productImage.Url = await fileUploadService.UploadFile(fileName, request.ImageData, AssetType.PRODUCT_IMG);
                }

                mapper.Map(request, productImage);
                productImageRepository.Update(productImage!);
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