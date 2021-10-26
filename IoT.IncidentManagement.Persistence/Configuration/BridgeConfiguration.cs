using IoT.IncidentManagement.Contstants;
using IoT.IncidentManagement.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static IoT.IncidentManagement.Contstants.ApplicationConstants;

namespace IoT.IncidentManagement.Persistence.Configuration
{
    public class BridgeConfiguration : IEntityTypeConfiguration<Bridge>
    {
        public void Configure(EntityTypeBuilder<Bridge> builder)
        {
            builder
                .Property(e => e.BridgeType).IsRequired().HasMaxLength(BridgeTypeMaxLen);
        }
    }
}
