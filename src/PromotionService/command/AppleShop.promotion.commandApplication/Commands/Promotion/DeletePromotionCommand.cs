using AppleShop.Share.Abstractions;

namespace AppleShop.promotion.commandApplication.Commands.Promotion
{
    public class DeletePromotionCommand : ICommand
    {
        public int? Id { get; set; }
    }
}