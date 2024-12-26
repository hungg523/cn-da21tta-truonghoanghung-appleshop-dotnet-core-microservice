namespace AppleShop.Share.Events.User.Request
{
    public class GetInfoUserEvent
    {
        public List<int?> UserId { get; set; } = new List<int?>();
        public List<int?> UserAddressId { get; set; } = new List<int?>();
    }
}