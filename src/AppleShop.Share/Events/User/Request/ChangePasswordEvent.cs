using AppleShop.Share.Events.Common;

namespace AppleShop.Share.Events.User.Request
{
    public class ChangePasswordEvent : BaseEvent
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}