using Rental.WebApi.Shared.Configurations.AppSettings.Interfaces;
using Rental.WebApi.Shared.DependencyInjection;

namespace Rental.WebApi
{
    public class Program
    {
        protected Program() { }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var appSettings = AppSettingsBootStrapper.Register(builder.Services);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseSwagger();

            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            RunServicesBootStrappers(builder, appSettings);

            app.MapControllers();

            app.Run();
        }

        private static void RunServicesBootStrappers(WebApplicationBuilder builder, IAppSettings appSettings)
        {
            AdministratorBootstrapper.Register(builder.Services, appSettings);
            DatabaseBootStrapper.Register(builder.Services, appSettings);
            DeliverymanBootStrapper.Register(builder.Services, appSettings);
            MessageBusBootStrapper.Register(builder.Services, appSettings);
            RentalBootStrapper.Register(builder.Services, appSettings);
        }
    }
}