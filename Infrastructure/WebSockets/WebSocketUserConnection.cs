using Domain.Model;
using Domain.Model.UserConnection;
using System;
using System.Net.WebSockets;
using System.Text;

namespace Infrastructure.WebSockets
{
    public class WebSocketUserConnection : UserConnection
    {
        private readonly UserIdentifier _userId;

        private readonly WebSocket _webSocket;
        public WebSocketUserConnection(WebSocket webSocket, UserIdentifier userId)
        {
            _webSocket = webSocket;
            _userId = userId;
            _receiver(webSocket);
        }

        public override UserIdentifier GetUserId()
            => _userId;

        public async override Task<bool> TrySendAsync(ArraySegment<byte> message)
        {
            if (_webSocket.State == WebSocketState.Open)
            {
                await _webSocket.SendAsync(message, WebSocketMessageType.Text, true, CancellationToken.None);
                return true;
            }
            return false;
        }

        private async Task _receiver(WebSocket webSocket)
        {
            while (webSocket.State == WebSocketState.Open)
            {
                var buffer = new byte[1024];
                var data = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            _lock.EnterReadLock();
            try
            {
                _connectionAbortedHandler?.Invoke();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }
    }
}
