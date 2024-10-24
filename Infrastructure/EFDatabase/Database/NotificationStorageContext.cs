using Domain.Model.Notification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Infrastructure.EFDatabase.Database
{
    internal class NotificationStorageContext : DbContext
    {
        public DbSet<NotificationStored> Notifications { get; set; }


        public NotificationStorageContext() : base() { }
        public NotificationStorageContext(DbContextOptions<NotificationStorageContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=NotificationStorage;Username=postgres;Password=1234");
            //используется только при миграциях

            base.OnConfiguring(optionsBuilder);
        }
    }
}
