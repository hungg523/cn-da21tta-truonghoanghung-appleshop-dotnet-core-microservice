using AppleShop.category.Domain.Abstractions.IRepositories;
using AppleShop.category.queryApplication.Queries.Category;
using AppleShop.Share.Shared;
using MediatR;
using Entities = AppleShop.category.Domain.Entities;

namespace AppleShop.category.queryApplication.Handler.Category
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, Result<List<Entities.Category>>>
    {
        private readonly ICategoryRepository categoryRepository;

        public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<Result<List<Entities.Category>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = categoryRepository.FindAll().ToList();
            return Result<List<Entities.Category>>.Ok(categories);
        }
    }
}