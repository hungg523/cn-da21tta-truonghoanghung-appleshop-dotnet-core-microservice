namespace AppleShop.Share.Events.User.Response
{
    public class GetInfoUserResponse
    {
        public ICollection<UserResponse> Users { get; set; } = new List<UserResponse>();
        public ICollection<UserAddressResponse> UserAddresses { get; set; } = new List<UserAddressResponse>();
    }
}