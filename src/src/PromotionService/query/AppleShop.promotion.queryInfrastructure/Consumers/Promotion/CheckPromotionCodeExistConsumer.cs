using AppleShop.promotion.Domain.Abstractions.IRepositories;
using AppleShop.Share.Events.Promotion.Request;
using AppleShop.Share.Events.Promotion.Response;
using MassTransit;

namespace AppleShop.promotion.queryInfrastructure.Consumers.Promotion
{
    public class CheckPromotionCodeExistConsumer : IConsumer<CheckPromotionCodeExistEvent>
    {
        private readonly IPromotionRepository promotionRepository;

        public CheckPromotionCodeExistConsumer(IPromotionRepository promotionRepository)
        {
            this.promotionRepository = promotionRepository;
        }

        public async Task Consume(ConsumeContext<CheckPromotionCodeExistEvent> context)
        {
            var message = context.Message;
            var promotion = await promotionRepository.FindSingleAsync(x => x.Code == message.Code || x.TimesUsed > 0 || x.EndDate < DateTime.Now, true);
            if (promotion is null) return;

            promotion.TimesUsed -= 1;
            promotionRepository.Update(promotion);
            await promotionRepository.SaveChangesAsync();

            await context.RespondAsync(new CheckPromotionCodeExistResponse 
            { 
                PromotionId = promotion.Id,
                DiscountPercentage = promotion.DiscountPercentage
            });
        }
    }
}