using Rental.WebApi.Shared.Configurations.AppSettings.Interfaces;
using Rental.WebApi.Shared.MessageBus;
using Rental.WebApi.Shared.MessageBus.Interfaces;

namespace Rental.WebApi.Shared.DependencyInjection
{
    public static class MessageBusBootStrapper
    {
        public static IServiceCollection Register(this IServiceCollection services, IAppSettings appSettings)
        {
            services.AddSingleton<IMessageBusService>(serviceProvider =>
                new MessageBusService(appSettings.RabbitMqMessageBus.ConnectionString));

            return services;
        }
    }
}
