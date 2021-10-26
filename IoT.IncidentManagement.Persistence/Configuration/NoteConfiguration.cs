using IoT.IncidentManagement.Contstants;
using IoT.IncidentManagement.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoT.IncidentManagement.Persistence.Configuration
{
    public class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.Property(b => b.Record).IsRequired().HasMaxLength(ApplicationConstants.IncidentNoteMaxLen);
            builder.HasIndex(b => b.IncidentId);
        }
    }
}
