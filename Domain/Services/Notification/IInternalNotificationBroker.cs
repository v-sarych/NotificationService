using Domain.Delegates.NotificationBroker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Notification
{
    public interface IInternalNotificationBroker
    {
        Task TryPush(ulong userId, byte[] payload, CantPushHandler cantPushHundler);

        Task<string> CreateQueue(ulong userId);// returns queue name
        Task Subscribe(string queueName, MessageHandler messageHandler);
    }
}
