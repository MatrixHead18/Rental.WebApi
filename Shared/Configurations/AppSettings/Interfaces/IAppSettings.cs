using Rental.WebApi.Shared.Configurations.AppSettings.Models;

namespace Rental.WebApi.Shared.Configurations.AppSettings.Interfaces
{
    public interface IAppSettings
    {
        public RabbitMQMessageBus RabbitMqMessageBus { get; set; }
        public MongoDatabase MongoDatabase { get; set; }
    }
}
