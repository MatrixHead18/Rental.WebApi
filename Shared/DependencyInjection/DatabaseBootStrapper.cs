using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Rental.WebApi.Shared.Configurations.AppSettings.Interfaces;
using Rental.WebApi.Shared.Data;

namespace Rental.WebApi.Shared.DependencyInjection
{
    public static class DatabaseBootStrapper
    {
        public static void Register(IServiceCollection services, IAppSettings appSettings)
        {
            services.AddDbContext<DatabaseContext>((serviceProvider,options) => 
            {
                options
                    .UseLoggerFactory(serviceProvider.GetService<ILoggerFactory>())
                    .UseMongoDB(new MongoClient(appSettings.MongoDatabase.ConnectionString), appSettings.MongoDatabase.DatabaseName)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            });
        }
    }
}
