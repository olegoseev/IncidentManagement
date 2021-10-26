using IoT.IncidentManagement.Contstants;
using IoT.IncidentManagement.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoT.IncidentManagement.Persistence.Configuration
{
    public class ClosureActionConfiguration : IEntityTypeConfiguration<ClosureAction>
    {
        public void Configure(EntityTypeBuilder<ClosureAction> builder)
        {
            builder.HasKey(b => b.IncidentId);
            builder.HasIndex(b => b.IncidentId);
            builder.Property(p => p.ToDoList).IsRequired().HasMaxLength(ApplicationConstants.ClosureActionMaxLen);
        }
    }
}
