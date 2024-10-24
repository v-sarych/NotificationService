using Domain.Repository;
using Infrastructure.EFDatabase.Database;
using Infrastructure.EFDatabase.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EFDatabase.Extentions
{
    internal static class AddEFDatabaseExtention
    {
        public static IServiceCollection AddEFDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<NotificationStorageContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("NotificationStorage"));
            });
            services.AddScoped<INotificationStorageRepository, EFNotificationStorage>();

            return services;
        }
    }
}
