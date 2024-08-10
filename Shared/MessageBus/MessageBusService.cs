using EasyNetQ;
using Polly;
using RabbitMQ.Client.Exceptions;
using Rental.WebApi.Shared.Domain;
using Rental.WebApi.Shared.MessageBus.Interfaces;

namespace Rental.WebApi.Shared.MessageBus
{
    public class MessageBusService : IMessageBusService
    {
        private IBus _bus;
        private IAdvancedBus _advancedBus;
        private readonly string _connectionString;

        public MessageBusService(string connectionString)
        {
            _connectionString = connectionString;

            TryConnect();
        }

        public bool IsConnected => _bus?.Advanced.IsConnected ?? false;

        public IAdvancedBus AdvancedBus => _bus?.Advanced;

        public void Dispose()
        {
            _bus.Dispose();
        }

        public async Task PublishAsync<T>(T message) where T : DomainEvent
        {
            TryConnect();
            await _bus.PubSub.PublishAsync(message);
        }

        public async void SubscribeAsync<T>(string subscriptionId, Func<T, Task> onMessage) where T : class
        {
            TryConnect();
            await _bus.PubSub.SubscribeAsync(subscriptionId, onMessage);
        }

        private void TryConnect()
        {
            if (IsConnected) return;

            var policy = Policy.Handle<EasyNetQException>()
                .Or<BrokerUnreachableException>()
                .WaitAndRetry(5, retryAttempt =>
                            TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            policy.Execute(() => {
                _bus = RabbitHutch.CreateBus(_connectionString);
                _advancedBus = _bus.Advanced;
                _advancedBus.Disconnected += OnDisconnect;
            });
        }

        private void OnDisconnect(object s, EventArgs e)
        {
            var policy = Policy.Handle<EasyNetQException>()
                 .Or<BrokerUnreachableException>()
                 .RetryForever();

            policy.Execute(TryConnect);
        }
    }
}
