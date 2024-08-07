using MongoDB.Driver;

namespace Rental.WebApi.Shared.DependencyInjection
{
    public static class DatabaseBootStrapper
    {
        public static void Register(IServiceCollection services, IAppSettings appSettings)
        {
            services.AddSingleton(serviceProvider =>
            {
                var settings = MongoClientSettings.FromConnectionString(appSettings.Database.ConnectionString);

                return new MongoClient(settings).GetDatabase(appSettings.Database.Name);
            });
        }
    }
}
