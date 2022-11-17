using App.Shared.Messages;

namespace App.Shared.Interfaces
{
    public interface IEventConsumer<TMessage, TPrimaryKey>
        where TMessage : class, IMessage<TPrimaryKey>
    {
        void Subscribe();
        void Unsubscribe();
    }
}
