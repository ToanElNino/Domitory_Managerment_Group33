using App.Shared.Messages;

namespace App.Shared.Interfaces
{
    public interface IEventPublisher<in TMessage> where TMessage : class
    {
        void Publish(TMessage message);
    }
}
