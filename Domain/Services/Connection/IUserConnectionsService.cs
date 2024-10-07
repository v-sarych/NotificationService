﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Connection
{
    public interface IUserConnectionsService
    {
        Task SendMessage(ulong userId);
        Task<int> UserConnectionsCount(ulong userId);
        Task AddConnection(IUserConnection connection, ulong userId);
    }
}
