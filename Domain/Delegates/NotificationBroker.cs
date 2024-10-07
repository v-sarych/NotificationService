using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Delegates.NotificationBroker
{
    public delegate Task CantPushHandler();
    public delegate Task MessageHandler(byte[] message);
}
