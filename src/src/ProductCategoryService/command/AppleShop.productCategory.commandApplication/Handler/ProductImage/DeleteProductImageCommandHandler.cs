using AppleShop.productCategory.commandApplication.Commands.ProductImage;
using AppleShop.productCategory.commandApplication.Validator.ProductImage;
using AppleShop.productCategory.Domain.Abstractions.IRepositories;
using AppleShop.Share.Exceptions;
using AppleShop.Share.Shared;
using AutoMapper;
using MediatR;
using Entities = AppleShop.productCategory.Domain.Entities;

namespace AppleShop.productCategory.commandApplication.Handler.ProductImage
{
    public class DeleteProductImageCommandHandler : IRequestHandler<DeleteProductImageCommand, Result<object>>
    {
        private readonly IProductImageRepository productImageRepository;
        private readonly IMapper mapper;

        public DeleteProductImageCommandHandler(IProductImageRepository productImageRepository, IMapper mapper)
        {
            this.productImageRepository = productImageRepository;
            this.mapper = mapper;
        }

        public async Task<Result<object>> Handle(DeleteProductImageCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteProductImageCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            using var transaction = await productImageRepository.BeginTransactionAsync(cancellationToken);
            try
            {
                var productImage = await productImageRepository.FindByIdAsync(request.Id);
                if (productImage is null) AppleException.ThrowNotFound(typeof(Entities.ProductImage));

                productImageRepository.Delete(productImage!);
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