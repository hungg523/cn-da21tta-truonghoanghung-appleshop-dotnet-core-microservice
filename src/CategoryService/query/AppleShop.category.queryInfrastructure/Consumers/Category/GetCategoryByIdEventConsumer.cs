using AppleShop.category.Domain.Abstractions.IRepositories;
using AppleShop.Share.Events.Category.Request;
using AppleShop.Share.Events.Category.Response;
using MassTransit;

namespace AppleShop.category.queryInfrastructure.Consumers.Category
{
    public class GetCategoryByIdEventConsumer : IConsumer<GetCategoryByIdEvent>
    {
        private readonly ICategoryRepository categoryRepository;

        public GetCategoryByIdEventConsumer(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task Consume(ConsumeContext<GetCategoryByIdEvent> context)
        {
            var message = context.Message;
            var category = await categoryRepository.FindByIdAsync(message.Id);
            if (category is null)
            {
                await context.RespondAsync(new CategoryResponse
                {
                    Id = null
                });
                return;
            }
            await context.RespondAsync(new CategoryResponse
            {
                Id = category.Id
            });
        }
    }
}