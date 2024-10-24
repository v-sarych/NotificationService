using Domain.Model;
using Domain.Model.Notification;
using Domain.Services.Notification;
using Infrastructure.RabbitMQ.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infrastructure.RabbitMQ.Implementations
{
    public class RabbitMQInternalNotificationBroker : IInternalNotificationBroker
    {
        private readonly InternalRabbitMQUnitOfWork _unitOfWork;
        private readonly IModel _rabbitmodel;

        public RabbitMQInternalNotificationBroker(InternalRabbitMQUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _rabbitmodel = unitOfWork.RabbitConnectionFactory.CreateConnection().CreateModel();
        }

        public Task<string> CreateQueue(UserIdentifier userId)
        {
            string queueName = _rabbitmodel.QueueDeclare(exclusive: true, autoDelete: true);

            _rabbitmodel.QueueBind(queueName, _unitOfWork.Configuration.InternalQueuesConfiguration.Exchange,
                _unitOfWork.Configuration.InternalQueuesConfiguration.BeginningOfBindingKey + "." + Convert.ToString(userId));

            return Task.FromResult(queueName);
        }

        public Task DeleteQueue(string name)
        {
            _rabbitmodel.QueueDelete(name);

            return Task.CompletedTask;
        }

        public Task Subscribe(string queueName, Func<InternalNotification, Task> messageHandler)
        {
            var consumer = new EventingBasicConsumer(_rabbitmodel);
            consumer.Received += async (model, ea) =>
            {
                var normilizedMessage = InternalNotification.FromByteArray(ea.Body.ToArray());
                await messageHandler(normilizedMessage);
            };

            _rabbitmodel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);

            return Task.CompletedTask;
        }

        public Task TryPush(UserIdentifier userId, byte[] payload, Func<Task> cantPushHundler)
        {
            _rabbitmodel.BasicPublish(exchange: _unitOfWork.Configuration.InternalQueuesConfiguration.Exchange,
                    routingKey: _unitOfWork.Configuration.InternalQueuesConfiguration.BeginningOfBindingKey + "." + Convert.ToString(userId),
                    basicProperties: null,
                    body: payload, 
                    mandatory: true);

            _rabbitmodel.BasicReturn += async (sender, ea) =>
            {
                await cantPushHundler();
            };

            return Task.CompletedTask;
        }
    }
}
