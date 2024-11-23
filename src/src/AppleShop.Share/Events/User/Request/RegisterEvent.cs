using AppleShop.Share.Events.Common;

namespace AppleShop.Share.Events.User.Request
{
    public class RegisterEvent : BaseEvent
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}