using AppleShop.Share.Events.Common;

namespace AppleShop.Share.Events.User.Request
{
    public class ResendOTPEvent : BaseEvent
    {
        public string? Email { get; set; }
    }
}