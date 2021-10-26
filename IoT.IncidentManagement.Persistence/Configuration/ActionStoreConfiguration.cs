using IoT.IncidentManagement.Contstants;
using IoT.IncidentManagement.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoT.IncidentManagement.Persistence.Configuration
{
    public class ActionStoreConfiguration : IEntityTypeConfiguration<ActionStore>
    {
        public void Configure(EntityTypeBuilder<ActionStore> builder)
        {
            builder.Property(p => p.Description).IsRequired().HasMaxLength(ApplicationConstants.ActionDescription);
            builder.Property(p => p.Order).IsRequired();
            builder.Property(p => p.State).HasConversion<string>().HasMaxLength(20).IsRequired();
        }
    }
}
