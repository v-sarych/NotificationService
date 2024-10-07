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
using static System.Runtime.InteropServices.JavaScript.JSType;

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

            await _notificationBroker.Subscribe(queueName, _brokerNotificationHandler);
        }

        private async Task _brokerNotificationHandler(NotificationForwarded message)
        {
            await _userConnection.SendAsync(new ArraySegment<byte>(message.Data, 0, message.Data.Length));
        }
    }
}
