namespace Rental.WebApi.Shared.Configurations.AppSettings.Models
{
    public class RabbitMQMessageBus
    {
        public string ConnectionString { get; set; }
        public string BrokerName { get; set; }
    }
}
