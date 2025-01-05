namespace AppleShop.Share.Events.User.Response
{
    public class UserAddressResponse
    {
        public int? UserAddressId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? AddressLine { get; set; }
        public string? Province { get; set; }
        public string? District { get; set; }
    }
}