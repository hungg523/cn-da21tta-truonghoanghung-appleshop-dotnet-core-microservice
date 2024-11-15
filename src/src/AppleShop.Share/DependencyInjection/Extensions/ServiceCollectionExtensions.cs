using AppleShop.Share.Abstractions;
using AppleShop.Share.Constant;
using AppleShop.Share.DependencyInjection.Options;
using AppleShop.Share.Service;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AppleShop.Share.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration, Assembly? consumersAssembly = null)
        {
            RabbitMqOptions rabbitMqOptions = new();
            configuration.GetSection(Const.BROKER_CONFIG).Bind(rabbitMqOptions);

            services.AddMassTransit(busConfigurator =>
            {
                if (consumersAssembly is not null) busConfigurator.AddConsumers(consumersAssembly);

                //ex: get-sample-by-id-request
                busConfigurator.SetKebabCaseEndpointNameFormatter();

                //Using rabbitMq for message broker
                busConfigurator.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(new Uri(rabbitMqOptions.Host), host =>
                    {
                        host.Username(rabbitMqOptions.Username);
                        host.Password(rabbitMqOptions.Password);
                        host.RequestedConnectionTimeout(TimeSpan.FromSeconds(10));
                    });

                    configurator.ConfigureEndpoints(context);
                    configurator.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(15)));
                    //configurator.UseDelayedRedelivery(r => r.Intervals(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(30)));
                });
            });

            return services;
        }

        public static IServiceCollection AddFileServices(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddScoped<IFileService, FileService>();

            return services;
        }
    }
}