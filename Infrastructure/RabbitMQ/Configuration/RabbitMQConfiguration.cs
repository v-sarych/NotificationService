using RabbitMQ.Client;

namespace Infrastructure.RabbitMQ.Configuration
{
    public class RabbitMQConfiguration
    {
        public ServerConfiguration ServerConfiguration { get; set; }
        public InternalQueuesConfiguration InternalQueuesConfiguration { get; set; }
    }
}
