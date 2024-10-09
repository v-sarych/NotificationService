using Infrastructure.RabbitMQ.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System.Text.Json;

namespace Infrastructure.RabbitMQ.Extentions
{
    public static class AddRabbitMQConfigurationExtention
    {
        public static IServiceCollection AddRabbitMQConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitConfiguration = JsonSerializer.Deserialize<RabbitMQConfiguration>(configuration["InfrastructureRabbitMQ"]);

            var unitOfWork = new InternalRabbitMQUnitOfWork()
            {
                Configuration = rabbitConfiguration,
                RabbitConnectionFactory = new ConnectionFactory()
                {
                    HostName = rabbitConfiguration.ServerConfiguration.Host,
                }
            };
            services.AddSingleton(unitOfWork);

            return services;
        }
    }
}
