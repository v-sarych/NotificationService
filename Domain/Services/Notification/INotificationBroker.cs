using Domain.Delegates.NotificationBroker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Notification
{
    public interface INotificationBroker
    {
        Task TryPush(ulong userId, byte[] payload, CantPushHandler cantPushHundler);

        Task<string> CreateQueue();// returns queue name
        Task Subscribe(string queueName, MessageHandler messageHandler);
    }
}
