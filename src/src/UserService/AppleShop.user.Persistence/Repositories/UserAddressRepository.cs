using AppleShop.user.Domain.Abstractions.IRepositories;
using AppleShop.user.Domain.Entities;
using AppleShop.user.Persistence.Repositories.Base;

namespace AppleShop.user.Persistence.Repositories
{
    public class UserAddressRepository(ApplicationDbContext context) : GenericRepository<UserAddress>(context), IUserAddressRepository
    {
    }
}