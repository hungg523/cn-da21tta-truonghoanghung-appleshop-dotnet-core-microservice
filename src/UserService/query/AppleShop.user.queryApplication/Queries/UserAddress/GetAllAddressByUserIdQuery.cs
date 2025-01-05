using AppleShop.Share.Abstractions;
using Entities = AppleShop.user.Domain.Entities;

namespace AppleShop.user.queryApplication.Queries.UserAddress
{
    public class GetAllAddressByUserIdQuery : IQuery<List<Entities.UserAddress>>
    {
        public int? UserId { get; set; }
    }
}