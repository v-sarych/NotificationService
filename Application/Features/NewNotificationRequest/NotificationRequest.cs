using Domain.Model;
using MediatR;

namespace Application.Features.NewNotificationRequest
{
    public class NotificationRequest : IRequest
    {
        public UserIdentifier UserId { get; set; }
        public byte[] Payload { get; set; }
    }
}
