using Rental.WebApi.Shared.Domain.Interfaces;
using Rental.WebApi.Shared.MessageBus.Message;

namespace Rental.WebApi.Shared.Domain.Objects
{
    public class Entity : IEntityModel
    {
        public Guid Id { get; set; }

        public Entity()
        {
            Id = Guid.NewGuid();
        }

        private List<Event> _notifications;
        public IReadOnlyCollection<Event> Notificacoes => _notifications.AsReadOnly();

        public void AddEvent(Event evento)
        {
            _notifications = _notifications ?? new List<Event>();
            _notifications.Add(evento);
        }

        public void RemoveEvent(Event eventItem)
        {
            _notifications?.Remove(eventItem);
        }

        public void ClearEvents()
        {
            _notifications?.Clear();
        }
    }
}
