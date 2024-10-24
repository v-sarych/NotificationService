using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Infrastructure.RabbitMQ.Extentions;
using Infrastructure.EFDatabase.Extentions;

namespace Infrastructure.Extentions
{
    public static class AddInfrastructureExtention
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
        {
            //RabbitMQ
            services.AddRabbitMQConfiguration(configuration);
            services.AddRabbitMQImplementations();

            //EF database
            services.AddEFDatabase(configuration);

            return services;
        }
    }
}
