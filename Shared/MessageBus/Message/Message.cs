namespace Rental.WebApi.Shared.MessageBus.Message
{
    public abstract class Message
    {
        protected Message()
        {
            MessageType = GetType().Name;
        }

        public string MessageType { get; protected set; }
    }

}
