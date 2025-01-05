using AppleShop.Share.Abstractions;
using Entities = AppleShop.user.Domain.Entities;

namespace AppleShop.user.queryApplication.Queries.UserAddress
{
    public class GetUserAddressByIdQuery : IQuery<Entities.UserAddress>
    {
        public int? Id { get; set; }
    }
}