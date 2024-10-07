using Domain.Services.Connection;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserConnect
{
    public class UserConnectionRequest : IRequest
    {
        public IUserConnection UserConnection {  get; set; }
        public ulong UserId { get; set; }
    }
}
