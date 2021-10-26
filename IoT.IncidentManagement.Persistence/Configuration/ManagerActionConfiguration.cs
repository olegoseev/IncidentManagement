using IoT.IncidentManagement.Contstants;
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
    public class ManagerActionConfiguration : IEntityTypeConfiguration<ManagerAction>
    {
        public void Configure(EntityTypeBuilder<ManagerAction> builder)
        {
            builder.Property(p => p.Description).IsRequired().HasMaxLength(ApplicationConstants.ActionDescription);
            builder.Property(p => p.Order).IsRequired();
            builder.Property(p => p.State).HasConversion<string>().HasMaxLength(20).IsRequired();
        }
    }
}
