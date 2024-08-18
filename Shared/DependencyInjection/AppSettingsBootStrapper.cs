using Rental.WebApi.Shared.Configurations.AppSettings;
using Rental.WebApi.Shared.Configurations.AppSettings.Interfaces;

namespace Rental.WebApi.Shared.DependencyInjection
{
    public static class AppSettingsBootStrapper
    {
        public static IAppSettings Register(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();

            AppSettings appSettings = new AppSettings();

            services.AddSingleton(appSettings);

            return appSettings; 
        }
    }
}
