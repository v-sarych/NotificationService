using Domain.Model;
using Domain.Model.UserConnection;
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
        public UserConnection UserConnection {  get; set; }
        public UserIdentifier UserId { get; set; }
    }
}
