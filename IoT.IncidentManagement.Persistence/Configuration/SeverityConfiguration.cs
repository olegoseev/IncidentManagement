using IoT.IncidentManagement.Contstants;
using IoT.IncidentManagement.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoT.IncidentManagement.Persistence.Configuration
{
    public class SeverityConfiguration : IEntityTypeConfiguration<Severity>
    {
        public void Configure(EntityTypeBuilder<Severity> builder)
        {
            builder.Property(p => p.IncidentSeverity).IsRequired()
                .HasMaxLength(ApplicationConstants.SeverityMaxLen);

            builder.Property(p => p.NotificationInterval).IsRequired();
        }
    }
}
