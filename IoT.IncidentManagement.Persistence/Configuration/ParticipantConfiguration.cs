using IoT.IncidentManagement.Contstants;
using IoT.IncidentManagement.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoT.IncidentManagement.Persistence.Configuration
{
    public class ParticipantConfiguration : IEntityTypeConfiguration<Participant>
    {
        public void Configure(EntityTypeBuilder<Participant> builder)
        {
            builder.HasKey(b => b.IncidentId);
            builder.HasIndex(b => b.IncidentId);
            builder.Property(p => p.Group).IsRequired().HasMaxLength(ApplicationConstants.ParticipantMaxLen);
        }
    }
}
