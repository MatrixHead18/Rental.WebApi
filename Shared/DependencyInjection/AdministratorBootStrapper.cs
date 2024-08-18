using Rental.WebApi.Features.Administrator.Adapters.Repositories;
using Rental.WebApi.Features.Administrator.Application.Interfaces;
using Rental.WebApi.Features.Administrator.Application.Services;
using Rental.WebApi.Features.Administrator.Domain.Interfaces;
using Rental.WebApi.Shared.Configurations.AppSettings.Interfaces;
using Rental.WebApi.Shared.Mediator;

namespace Rental.WebApi.Shared.DependencyInjection
{
    public static class AdministratorBootstrapper
    {
        public static IServiceCollection Register(this IServiceCollection services, IAppSettings appSettings)
        {
            services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
            services.AddScoped<IAdministratorServices, AdministratorServices>();

            services.AddScoped<IMediatorHandler, MediatorHandler>();

            return services;
        }
    }
}
