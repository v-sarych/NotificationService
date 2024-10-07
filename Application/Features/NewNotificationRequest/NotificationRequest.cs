using MediatR;

namespace Application.Features.NewNotificationRequest
{
    public class NotificationRequest : IRequest
    {
        public ulong UserId { get; set; }
        public byte[] Payload { get; set; }
    }
}
