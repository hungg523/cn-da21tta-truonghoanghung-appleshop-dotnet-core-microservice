using AppleShop.Share.Abstractions;
using AppleShop.user.queryApplication.Queries.DTOs;

namespace AppleShop.user.queryApplication.Queries.User
{
    public class GetProfileUserByIdQuery : IQuery<UserDTO>
    {
        public int? Id { get; set; }
    }
}