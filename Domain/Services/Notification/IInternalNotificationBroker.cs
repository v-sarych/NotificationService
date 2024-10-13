using Domain.Model.Notification;

namespace Domain.Services.Notification
{
    public interface IInternalNotificationBroker
    {
        Task TryPush(ulong userId, byte[] payload, Func<Task> cantPushHandler);

        Task<string> CreateQueue(ulong userId);
        Task Subscribe(string queueName, Func<InternalNotification, Task> messageHandler);
    }
}
