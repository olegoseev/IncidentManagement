using IoT.IncidentManagement.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Persistence.Configuration
{
    public class NotificationStoreConfiguration : IEntityTypeConfiguration<NotificationStore>
    {
        public void Configure(EntityTypeBuilder<NotificationStore> builder)
        {
            builder.Property(p => p.Group).HasConversion<string>().HasMaxLength(20).IsRequired();
            builder.Property(p => p.Type).HasConversion<string>().HasMaxLength(20).IsRequired();
            builder.Property(p => p.State).HasConversion<string>().HasMaxLength(20).IsRequired();
        }
    }
}
