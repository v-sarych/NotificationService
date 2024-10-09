using Domain.Services.Notification;
using Infrastructure.RabbitMQ.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.RabbitMQ.Extentions
{
    public static class AddRabbitMQImplementationsExtention
    {
        public static IServiceCollection AddRabbitMQImplementations(this IServiceCollection services)
        {
            services.AddTransient<IInternalNotificationBroker, RabbitMQInternalNotificationBroker>();

            return services;
        }
    }
}
