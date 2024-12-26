using AppleShop.category.Domain.Abstractions.IRepositories;
using AppleShop.category.queryApplication.Queries.Category;
using AppleShop.category.queryApplication.Validator.Category;
using AppleShop.Share.Exceptions;
using AppleShop.Share.Shared;
using MediatR;
using Entities = AppleShop.category.Domain.Entities;

namespace AppleShop.category.queryApplication.Handler.Category
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Result<Entities.Category>>
    {
        private readonly ICategoryRepository categoryRepository;

        public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<Result<Entities.Category>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetCategoryByIdQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) AppleException.ThrowValidation(validationResult);

            var category = await categoryRepository.FindByIdAsync(request.Id);
            if (category is null) AppleException.ThrowNotFound(typeof(Entities.Category));
            return Result<Entities.Category>.Ok(category);
        }
    }
}