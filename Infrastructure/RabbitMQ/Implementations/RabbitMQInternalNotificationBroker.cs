using Domain.Delegates.NotificationBroker;
using Domain.Model.Notification;
using Domain.Services.Notification;
using Infrastructure.RabbitMQ.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Reflection;
using System.Text;
using System.Threading.Channels;

namespace Infrastructure.RabbitMQ.Implementations
{
    public class RabbitMQInternalNotificationBroker : IInternalNotificationBroker
    {
        private readonly InternalRabbitMQUnitOfWork _unitOfWork;
        private readonly IModel _rabbitmodel;

        public RabbitMQInternalNotificationBroker(InternalRabbitMQUnitOfWork unitOfWork)
        {
            unitOfWork = unitOfWork;
            _rabbitmodel = unitOfWork.RabbitConnectionFactory.CreateConnection().CreateModel();
        }

        public async Task<string> CreateQueue(ulong userId)
        {
            string queueName = _rabbitmodel.QueueDeclare(exclusive: true, autoDelete: true);

            _rabbitmodel.QueueBind(queueName, _unitOfWork.Configuration.InternalQueuesConfiguration.Exchange,
                _unitOfWork.Configuration.InternalQueuesConfiguration.BeginningOfBindingKey + "." + Convert.ToString(userId));

            return queueName;
        }

        public async Task Subscribe(string queueName, MessageHandler messageHandler)
        {
            var consumer = new EventingBasicConsumer(_rabbitmodel);
            consumer.Received += async (model, ea) =>
            {
                var normilizedMessage = InternalNotification.FromByteArray(ea.Body.ToArray());
                await messageHandler.Invoke(normilizedMessage);
            };

            _rabbitmodel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);
        }

        public async Task TryPush(ulong userId, byte[] payload, CantPushHandler cantPushHundler)
        {
            _rabbitmodel.BasicPublish(exchange: _unitOfWork.Configuration.InternalQueuesConfiguration.Exchange,
                    routingKey: _unitOfWork.Configuration.InternalQueuesConfiguration.BeginningOfBindingKey + "." + Convert.ToString(userId),
                    basicProperties: null,
                    body: payload, 
                    mandatory: true);

            _rabbitmodel.BasicReturn += async (sender, ea) =>
            {
                await cantPushHundler.Invoke();
            };
        }
    }
}
