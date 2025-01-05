namespace AppleShop.Share.Events.Promotion.Response
{
    public class GetInfoPromotionResponse
    {
        public ICollection<PromotionResponse> Promotions { get; set; } = new List<PromotionResponse>();
    }
}