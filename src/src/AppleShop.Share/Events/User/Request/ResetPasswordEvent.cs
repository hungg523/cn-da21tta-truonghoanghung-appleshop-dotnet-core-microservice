using AppleShop.Share.Events.Common;

namespace AppleShop.Share.Events.User.Request
{
    public class ResetPasswordEvent : BaseEvent
    {
        public string? Email { get; set; }
    }
}