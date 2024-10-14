using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.UserConnection
{
    public delegate Task ConnectionAbortedHandler();
    public abstract class UserConnectionBase
    {
        public ConnectionAbortedHandler ConnectionAbortedHandler;
        public abstract Task SendAsync(byte[] message);
    }
}
