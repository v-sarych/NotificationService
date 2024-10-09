using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.RabbitMQ.Configuration
{
    public class InternalRabbitMQUnitOfWork
    {
        public IConnectionFactory RabbitConnectionFactory { get; set; }
        
        public RabbitMQConfiguration Configuration { get; set; }
    }
}
