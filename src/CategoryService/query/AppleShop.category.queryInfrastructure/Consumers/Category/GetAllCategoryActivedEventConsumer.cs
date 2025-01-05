using AppleShop.category.Domain.Abstractions.IRepositories;
using AppleShop.Share.Events.Category.Request;
using AppleShop.Share.Events.Category.Response;
using MassTransit;

namespace AppleShop.category.queryInfrastructure.Consumers.Category
{
    public class GetAllCategoryActivedEventConsumer : IConsumer<GetAllCategoryActivedEvent>
    {
        private readonly ICategoryRepository categoryRepository;

        public GetAllCategoryActivedEventConsumer(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task Consume(ConsumeContext<GetAllCategoryActivedEvent> context)
        {
            var categories = categoryRepository.FindAll(x => x.IsActived == 1).ToList();
            await context.RespondAsync(new CategoriesResponse { Ids = categories.Select(x => x.Id).ToList() });
        }
    }
}