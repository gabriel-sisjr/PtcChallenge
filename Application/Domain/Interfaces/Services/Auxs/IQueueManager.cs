using Domain.Enums;

namespace Domain.Interfaces.Services.Auxs
{
    public interface IQueueManager
    {
        void SendMessage<T>(T content, QueueName qeueName);
    }
}
