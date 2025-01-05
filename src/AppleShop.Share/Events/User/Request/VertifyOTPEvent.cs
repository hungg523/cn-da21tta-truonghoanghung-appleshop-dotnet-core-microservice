using AppleShop.Share.Events.Common;

namespace AppleShop.Share.Events.User.Request
{
    public class VertifyOTPEvent : BaseEvent
    {
        public string? Email { get; set; }
        public string? OTP { get; set; }
    }
}