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
    public class CreateProductCommandHanler : IRequestHandler<CreateProductCommand, Result<object>>
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public CreateProductCommandHanler(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        public async Task<Result<object>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateProductCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);
            
            if(request.CategoryId is not null)
            {
                var category = await categoryRepository.FindByIdAsync(request.CategoryId!);
                if (category is null) AppleException.ThrowNotFound(typeof(Entities.Category));
            }

            var product = mapper.Map<Entities.Product>(request);
            using var transaction = await productRepository.BeginTransactionAsync(cancellationToken);
            try
            {
                productRepository.Create(product);
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