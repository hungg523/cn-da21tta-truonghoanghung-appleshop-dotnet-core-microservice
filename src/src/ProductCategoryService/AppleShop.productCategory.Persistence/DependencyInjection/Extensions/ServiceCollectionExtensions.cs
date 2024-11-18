using AppleShop.productCategory.Domain.Abstractions.IRepositories;
using AppleShop.productCategory.Domain.Abstractions.IRepositories.Base;
using AppleShop.productCategory.Persistence.Repositories;
using AppleShop.productCategory.Persistence.Repositories.Base;
using AppleShop.Share.Constant;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppleShop.productCategory.Persistence.DependencyInjection.Extensions
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
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductImageRepository, ProductImageRepository>();
            services.AddScoped<IColorRepository, ColorRepository>();
            services.AddScoped<IProductColorRepository, ProductColorRepository>();
            return services;
        }
    }
}