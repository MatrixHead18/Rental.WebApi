using EasyNetQ;
using Rental.WebApi.Shared.Domain;

namespace Rental.WebApi.Shared.MessageBus.Interfaces
{
    public interface IMessageBusService : IDisposable
    {
        bool IsConnected { get; }
        IAdvancedBus AdvancedBus { get; }

        Task PublishAsync<T>(T message) where T : DomainEvent;

        void SubscribeAsync<T>(string subscriptionId, Func<T, Task> onMessage) where T : class;
    }
}
