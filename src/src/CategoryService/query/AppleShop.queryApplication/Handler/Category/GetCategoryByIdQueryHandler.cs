using Entities = AppleShop.Domain.Entities;
using AppleShop.queryApplication.Queries.Category;
using AppleShop.Share.Shared;
using MediatR;
using AppleShop.Domain.Abstractions.IRepositories;
using AppleShop.Share.Exceptions;
using AppleShop.queryApplication.Validator.Category;

namespace AppleShop.queryApplication.Handler.Category
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
            return category;
        }
    }
}