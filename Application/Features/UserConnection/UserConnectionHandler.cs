using Domain.Model.Notification;
using Domain.Model.UserConnection;
using Domain.Repository;
using Domain.Services.Notification;
using MediatR;

namespace Application.Features.UserConnect
{
    public class UserConnectionHandler : IRequestHandler<UserConnectionRequest>
    {
        private readonly INotificationStorageRepository _notificationStorage;
        private readonly IInternalNotificationBroker _notificationBroker;

        public UserConnectionHandler(INotificationStorageRepository notificationStorage, IInternalNotificationBroker notificationBroker)
        {
            _notificationStorage = notificationStorage;
            _notificationBroker = notificationBroker;
        }

        public async Task Handle(UserConnectionRequest request, CancellationToken cancellationToken)
        {
            string queueName = await _notificationBroker.CreateQueue(request.UserId);

            var oldNotifications = await _notificationStorage.GetSortedByDate(request.UserId);
            foreach (var notification in oldNotifications)
                if(!await request.UserConnection.TrySendAsync(new ArraySegment<byte>(notification.Payload, 0, notification.Payload.Length)))
                    return;

            await _notificationBroker.Subscribe(queueName, (message) => _brokerNotificationHandler(message, request.UserConnection));
        }

        private async Task _brokerNotificationHandler(InternalNotification message, UserConnection userConnection)
        {
            await userConnection.TrySendAsync(new ArraySegment<byte>(message.Data, 0, message.Data.Length));
        }
    }
}
