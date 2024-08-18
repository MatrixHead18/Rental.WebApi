using Rental.WebApi.Features.Lease.Adapters.Repositories;
using Rental.WebApi.Features.Lease.Application.Interfaces;
using Rental.WebApi.Features.Lease.Application.Services;
using Rental.WebApi.Features.Lease.Domain.Interfaces;
using Rental.WebApi.Shared.Configurations.AppSettings.Interfaces;

namespace Rental.WebApi.Shared.DependencyInjection
{
    public static class RentalBootStrapper
    {
        public static IServiceCollection Register(this IServiceCollection services, IAppSettings appSettings)
        {
            services.AddScoped<IRentalRepository, RentalRepository>();
            services.AddScoped<ILeaseServices, LeaseService>();

            return services;
        }
    }
}
