using Domain.Delegates;
using System;

namespace Domain.Model.UserConnection
{
    public abstract class UserConnection
    {
        public ConnectionAbortedHandler? ConnectionAbortedHandler;

        public abstract Task SendAsync(ArraySegment<byte> message);
    }
}
