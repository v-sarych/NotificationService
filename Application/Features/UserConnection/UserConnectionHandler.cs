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

        private UserConnection _userConnection;

        public UserConnectionHandler(INotificationStorageRepository notificationStorage, IInternalNotificationBroker notificationBroker)
        {
            _notificationStorage = notificationStorage;
            _notificationBroker = notificationBroker;
        }

        public async Task Handle(UserConnectionRequest request, CancellationToken cancellationToken)
        {
            _userConnection = request.UserConnection;
            string queueName = await _notificationBroker.CreateQueue(request.UserId);

            var oldNotifications = await _notificationStorage.GetSortedByDate(request.UserId);
            foreach (var notification in oldNotifications)
                await request.UserConnection.SendAsync(new ArraySegment<byte>(notification.Payload, 0, notification.Payload.Length));

            await _notificationBroker.Subscribe(queueName, (message) => _brokerNotificationHandler(message, _userConnection));
        }

        private async Task _brokerNotificationHandler(InternalNotification message, UserConnection userConnection)
        {
            await userConnection.SendAsync(new ArraySegment<byte>(message.Data, 0, message.Data.Length));
        }
    }
}
