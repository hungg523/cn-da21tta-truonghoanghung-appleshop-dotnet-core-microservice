using AppleShop.Share.DependencyInjection.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AppleShop.user.commandInfrastructure.DependencyInjection.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMasstransitEventDispatcher();
            services.AddRabbitMq(configuration, Assembly.GetExecutingAssembly());
            services.AddMailService(configuration);
            return services;
        }
    }
}