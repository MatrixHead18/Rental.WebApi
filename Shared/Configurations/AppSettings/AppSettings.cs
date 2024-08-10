using Rental.WebApi.Shared.Configurations.AppSettings.Interfaces;
using Rental.WebApi.Shared.Configurations.AppSettings.Models;

namespace Rental.WebApi.Shared.Configurations.AppSettings
{
    public class AppSettings : IAppSettings
    {
        public RabbitMQMessageBus RabbitMqMessageBus { get; set; }

        public AppSettings()
        {
            RabbitMqMessageBus = new RabbitMQMessageBus
            {
                ConnectionString = GetAndValidateEnvironment(""),
                BrokerName = GetAndValidateEnvironment("")
            };
        }

        private static string GetAndValidateEnvironment(string value)
        {
            var result = Environment.GetEnvironmentVariable(value);

            if (string.IsNullOrEmpty(result))
                throw new ArgumentException("Error fetching environment variable, cannot be null or empty", value);

            return result;
        }
    }
}
