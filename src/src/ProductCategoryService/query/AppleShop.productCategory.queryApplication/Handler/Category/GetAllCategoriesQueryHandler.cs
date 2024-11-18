using Entities = AppleShop.productCategory.Domain.Entities;
using AppleShop.productCategory.queryApplication.Queries.Category;
using MediatR;
using AppleShop.Share.Shared;
using AppleShop.productCategory.Domain.Abstractions.IRepositories;

namespace AppleShop.productCategory.queryApplication.Handler.Category
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