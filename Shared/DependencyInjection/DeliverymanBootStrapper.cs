using Rental.WebApi.Features.Deliveryman.Adapters.Repositories;
using Rental.WebApi.Features.Deliveryman.Application.Interfaces;
using Rental.WebApi.Features.Deliveryman.Application.Services;
using Rental.WebApi.Features.Deliveryman.Domain.Interfaces;
using Rental.WebApi.Shared.Configurations.AppSettings.Interfaces;

namespace Rental.WebApi.Shared.DependencyInjection
{
    public static class DeliverymanBootStrapper
    {
        public static IServiceCollection Register(this IServiceCollection services, IAppSettings appSettings)
        {
            services.AddScoped<IDeliverymanRepository, DeliverymanRepository>();
            services.AddScoped<IDeliverymanService, DeliverymanService>();

            return services;
        }
    }
}
