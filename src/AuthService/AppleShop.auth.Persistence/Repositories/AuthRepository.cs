using AppleShop.auth.Domain.Abstractions.IRepositories;
using AppleShop.auth.Domain.Entities;
using AppleShop.auth.Persistence.Repositories.Base;

namespace AppleShop.auth.Persistence.Repositories
{
    public class AuthRepository(ApplicationDbContext context) : GenericRepository<Auth>(context), IAuthRepository
    {
    }
}