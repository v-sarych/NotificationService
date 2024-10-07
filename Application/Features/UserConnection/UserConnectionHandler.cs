using Domain.Model.Notification;
using Domain.Repository;
using Domain.Services.Connection;
using Domain.Services.Notification;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserConnect
{
    public class UserConnectionHandler : IRequestHandler<UserConnectionRequest>
    {
        private readonly INotificationStorageRepository _notificationStorage;
        private readonly INotificationBroker _notificationBroker;

        private IUserConnection _userConnection;

        public UserConnectionHandler(INotificationStorageRepository notificationStorage, INotificationBroker notificationBroker)
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

            await _notificationBroker.Subscribe(queueName, async (data) =>
            {
                await _brokerNotificationHandler(new ArraySegment<byte>(data, 0, data.Length));
            });
        }

        private async Task _brokerNotificationHandler(ArraySegment<byte> message)
        {
            await _userConnection.SendAsync(message);
        }
    }
}
