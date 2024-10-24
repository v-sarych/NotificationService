using Domain.Model;
using Domain.Model.Notification;

namespace Domain.Services.Notification
{
    public interface IInternalNotificationBroker
    {
        Task TryPush(UserIdentifier userId, byte[] payload, Func<Task> cantPushHandler);

        Task<string> CreateQueue(UserIdentifier userId);
        Task DeleteQueue(string name);
        Task Subscribe(string queueName, Func<InternalNotification, Task> messageHandler);
    }
}
