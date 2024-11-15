using AppleShop.Domain.Abstractions.IRepositories;
using AppleShop.Domain.Abstractions.IRepositories.Base;
using AppleShop.Persistence.Repositories;
using AppleShop.Persistence.Repositories.Base;
using AppleShop.Share.Constant;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppleShop.Persistence.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(Const.CONN_CONFIG_SQL);
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            services.RegisterServices();
            return services;
        }

        private static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            return services;
        }
    }
}