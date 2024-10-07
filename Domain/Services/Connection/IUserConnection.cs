using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Connection
{
    public interface IUserConnection
    {
        Task SendAsync(ArraySegment<byte> message);
    }
}
