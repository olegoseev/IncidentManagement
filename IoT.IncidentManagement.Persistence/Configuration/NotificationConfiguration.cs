using IoT.IncidentManagement.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoT.IncidentManagement.Persistence.Configuration
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.Property(p => p.Interval).IsRequired();
            builder.Property(p => p.Type).HasConversion<string>().HasMaxLength(20).IsRequired();
            builder.Property(p => p.State).HasConversion<string>().HasMaxLength(20).IsRequired();
            builder.Property(p => p.Group).HasConversion<string>().HasMaxLength(20).IsRequired();
        }
    }
}
