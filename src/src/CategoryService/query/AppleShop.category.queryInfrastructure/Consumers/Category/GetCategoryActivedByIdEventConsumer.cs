using AppleShop.category.Domain.Abstractions.IRepositories;
using AppleShop.Share.Events.Category.Request;
using AppleShop.Share.Events.Category.Response;
using MassTransit;

namespace AppleShop.category.queryInfrastructure.Consumers.Category
{
    public class GetCategoryActivedByIdEventConsumer : IConsumer<GetCategoryActivedByIdEvent>
    {
        private readonly ICategoryRepository categoryRepository;

        public GetCategoryActivedByIdEventConsumer(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task Consume(ConsumeContext<GetCategoryActivedByIdEvent> context)
        {
            var message = context.Message;
            var category = await categoryRepository.FindSingleAsync(x => x.Id == message.Id && x.IsActived == 1);
            if (category is null)
            {
                await context.RespondAsync(new CategoryResponse { Id = null });
                return;
            }
            await context.RespondAsync(new CategoryResponse { Id = category.Id });
        }
    }
}