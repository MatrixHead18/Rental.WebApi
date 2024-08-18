using Rental.WebApi.Shared.MessageBus.Message;

namespace Rental.WebApi.Shared.Mediator
{
    public interface IMediatorHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
    }
}
