using AppleShop.commandApplication.Commands.Product;
using AppleShop.commandApplication.Validator.Product;
using AppleShop.Domain.Abstractions.IRepositories;
using AppleShop.Share.Exceptions;
using AppleShop.Share.Shared;
using AutoMapper;
using MediatR;
using Entities = AppleShop.Domain.Entities;

namespace AppleShop.commandApplication.Handler.Product
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<object>>
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public UpdateProductCommandHandler(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        public async Task<Result<object>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateProductCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            using var transaction = await productRepository.BeginTransactionAsync(cancellationToken);
            try
            {
                var product = await productRepository.FindByIdAsync(request.Id);
                if (product is null) AppleException.ThrowNotFound(typeof(Entities.Product));

                if (request.CategoryId is not null)
                {
                    var category = await categoryRepository.FindByIdAsync(request.CategoryId);
                    if (category is null) AppleException.ThrowNotFound(typeof(Entities.Category));
                }

                mapper.Map(request, product);
                productRepository.Update(product!);
                await productRepository.SaveChangesAsync(cancellationToken);
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