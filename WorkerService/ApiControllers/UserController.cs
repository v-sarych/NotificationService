using Application.Features.UserConnect;
using Domain.Model;
using Infrastructure.WebSockets;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WorkerService.ApiControllers
{
    [ApiController]
    [Route("Notifications/User")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet]
        public async Task OpenWebSocket(string id)
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                var ws = await HttpContext.WebSockets.AcceptWebSocketAsync();
                var conn = new WebSocketUserConnection(ws);

                var mediatrRequest = new UserConnectionRequest()
                {
                    UserConnection = conn,
                    UserId = new UserIdentifier(id)
                };

                await _mediator.Send(conn);
            }
        }
    }
}
