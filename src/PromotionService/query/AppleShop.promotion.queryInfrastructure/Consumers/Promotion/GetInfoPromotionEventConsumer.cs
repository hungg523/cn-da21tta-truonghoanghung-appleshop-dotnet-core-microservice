using AppleShop.promotion.Domain.Abstractions.IRepositories;
using AppleShop.Share.Events.Promotion.Request;
using AppleShop.Share.Events.Promotion.Response;
using MassTransit;

namespace AppleShop.promotion.queryInfrastructure.Consumers.Promotion
{
    public class GetInfoPromotionEventConsumer : IConsumer<GetInfoPromotionEvent>
    {
        private readonly IPromotionRepository promotionRepository;

        public GetInfoPromotionEventConsumer(IPromotionRepository promotionRepository)
        {
            this.promotionRepository = promotionRepository;
        }

        public async Task Consume(ConsumeContext<GetInfoPromotionEvent> context)
        {
            var message = context.Message;
            var promotions = promotionRepository.FindAll(x => message.Id.Contains(x.Id)).ToList();
            await context.RespondAsync(new GetInfoPromotionResponse
            {
                Promotions = promotions.Select(x => new PromotionResponse
                {
                    Id = x.Id,
                    DiscountPercentage = x.DiscountPercentage,
                    Discription = x.Description
                }).ToList()
            });
        }
    }
}