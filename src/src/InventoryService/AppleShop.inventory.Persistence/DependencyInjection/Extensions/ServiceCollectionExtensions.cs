﻿using AppleShop.inventory.Domain.Abstractions.IRepositories;
using AppleShop.inventory.Domain.Abstractions.IRepositories.Base;
using AppleShop.inventory.Persistence.Repositories;
using AppleShop.inventory.Persistence.Repositories.Base;
using AppleShop.Share.Constant;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppleShop.inventory.Persistence.DependencyInjection.Extensions
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
            services.AddScoped<IInventoryRepository, InventoryRepository>();
            return services;
        }
    }
}