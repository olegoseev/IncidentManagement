using IoT.IncidentManagement.Contstants;
using IoT.IncidentManagement.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoT.IncidentManagement.Persistence.Configuration
{
    public class IncidentConfiguration : IEntityTypeConfiguration<Incident>
    {
        public void Configure(EntityTypeBuilder<Incident> builder)
        {
            builder.HasIndex(b => b.IncidentCase);
            builder.Property(b => b.IncidentCase).IsRequired().HasMaxLength(ApplicationConstants.IncidentCaseMaxLen);
            builder.Property(b => b.Description).IsRequired().HasMaxLength(ApplicationConstants.IncidentDescriptionMaxLen);
            builder.Property(b => b.StartTime).IsRequired();
            builder.Property(b => b.NotifiedTime).IsRequired();
        }
    }
}
