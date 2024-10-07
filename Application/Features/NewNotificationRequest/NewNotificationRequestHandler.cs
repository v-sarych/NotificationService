using Domain.Repository;
using Domain.Services.Notification;
using MediatR;

namespace Application.Features.NewNotificationRequest
{
    public class NewNotificationRequestHandler : IRequestHandler<NotificationRequest>
    {
        private readonly INotificationStorageRepository _notificationStorage;
        private readonly INotificationBroker _notificationBroker;

        public NewNotificationRequestHandler(INotificationStorageRepository notificationStorageRepository, INotificationBroker notificationBroker)
            => (_notificationBroker, _notificationStorage) = (notificationBroker, notificationStorageRepository);

        public async Task Handle(NotificationRequest request, CancellationToken cancellationToken)
        {
            await _notificationBroker.TryPush(request.UserId, request.Payload, 
                async () => {
                    await _cantPushMessage(request);
                }
            );
        }

        private async Task _cantPushMessage(NotificationRequest request)
        {
            await _notificationStorage.Store(new Domain.Model.Notification.Notification()
            {
                Payload = request.Payload,
                UserId = request.UserId,
                DateOfCreation = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds()
            });
        }
    }
}
