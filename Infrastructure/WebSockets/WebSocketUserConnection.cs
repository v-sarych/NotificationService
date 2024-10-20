using Domain.Model.UserConnection;
using System;
using System.Net.WebSockets;
using System.Text;

namespace Infrastructure.WebSockets
{
    public class WebSocketUserConnection : UserConnection
    {
        private readonly WebSocket _webSocket;
        public WebSocketUserConnection(WebSocket webSocket)
        {
            _webSocket = webSocket;
            _receiver(webSocket);
        }
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
