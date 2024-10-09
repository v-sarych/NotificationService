using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.RabbitMQ.Configuration
{
    // это очереди привязанные к айди пользователя в которые сообщение из вне пересылаются
    public class InternalQueuesConfiguration
    {
        public string Exchange { get; set; }
        public string BeginningOfBindingKey { get; set; }
    }
}
