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
            string queueName = await _notificationBroker.CreateQueue(request.UserConnection.GetUserId());

            request.UserConnection.ConnectionAbortedHandler += async () => { await _userConnectionAbortedHandler(queueName); };

            var oldNotifications = await _notificationStorage.GetSortedByDate(request.UserConnection.GetUserId());
            foreach (var notification in oldNotifications)
                if(!await request.UserConnection.TrySendAsync(new ArraySegment<byte>(notification.Payload, 0, notification.Payload.Length)))
                    return;

            await _notificationBroker.Subscribe(queueName, (message) => _brokerNotificationHandler(message, request.UserConnection));
        }


        private async Task _userConnectionAbortedHandler(string queueName)
            => await _notificationBroker.DeleteQueue(queueName);


        private async Task _brokerNotificationHandler(InternalNotification message, UserConnection userConnection)
        {
            if (!await userConnection.TrySendAsync(new ArraySegment<byte>(message.Data, 0, message.Data.Length)))
            {
                var storing = new NotificationStored()
                {
                    UserId = userConnection.GetUserId(),
                    Payload = message.Data
                };
                storing.SetNowDateOfCreation();

                await _notificationStorage.Store(storing);
            }
        }
    }
}
