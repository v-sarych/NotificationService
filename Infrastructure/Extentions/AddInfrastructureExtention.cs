using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Infrastructure.RabbitMQ.Extentions;

namespace Infrastructure.Extentions
{
    public static class AddInfrastructureExtention
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
        {
            //RabbitMQ
            services.AddRabbitMQConfiguration(configuration);
            services.AddRabbitMQImplementations();

            //WebSockets


            return services;
        }
    }
}
