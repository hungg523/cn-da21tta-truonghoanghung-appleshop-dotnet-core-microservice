using AppleShop.category.Domain.Abstractions.IRepositories;
using AppleShop.category.queryApplication.Queries.Category;
using AppleShop.Share.Shared;
using MediatR;
using Entities = AppleShop.category.Domain.Entities;

namespace AppleShop.category.queryApplication.Handler.Category
{
    public class GetAllCategoriesActivedQueryHandler : IRequestHandler<GetAllCategoriesActivedQuery, Result<List<Entities.Category>>>
    {
        private readonly ICategoryRepository categoryRepository;

        public GetAllCategoriesActivedQueryHandler(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<Result<List<Entities.Category>>> Handle(GetAllCategoriesActivedQuery request, CancellationToken cancellationToken)
        {
            var categories = categoryRepository.FindAll(x => x.IsActived == 1).ToList();
            return Result<List<Entities.Category>>.Ok(categories);
        }
    }
}