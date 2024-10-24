using Domain.Model.Notification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EFDatabase.Database
{
    internal class NotificationConfiguration : IEntityTypeConfiguration<NotificationStored>
    {
        public void Configure(EntityTypeBuilder<NotificationStored> builder)
        {
            builder.OwnsOne()
        }
    }
}
