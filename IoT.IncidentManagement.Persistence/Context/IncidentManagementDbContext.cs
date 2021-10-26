using IoT.IncidentManagement.Domain.Common;
using IoT.IncidentManagement.Domain.Entities;
using IoT.IncidentManagement.Domain.Enums;

using Microsoft.EntityFrameworkCore;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.Persistence.Context
{
    public class IncidentManagementDbContext : DbContext
    {
        public IncidentManagementDbContext(DbContextOptions<IncidentManagementDbContext> options)
            : base(options)
        {

        }

        public DbSet<ActionStore> ActionsStore { get; set; }
        public DbSet<Bridge> Bridges { get; set; }
        public DbSet<ClosureAction> ClosureActions { get; set; }
        public DbSet<Incident> Incidents { get; set; }
        public DbSet<ManagerAction> ManagerActions { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationStore> NotificationsStore { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Severity> Severities { get; set; }
        public DbSet<Status> Statuses { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancelationToken = new CancellationToken())
        {

            foreach (var entry in ChangeTracker.Entries<Note>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.RecordTime = DateTime.UtcNow;
                }
            }

            foreach (var entry in ChangeTracker.Entries<AuditEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancelationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IncidentManagementDbContext).Assembly);

            SeedData(modelBuilder);
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            int BridgeId1 = 1;
            int BridgeId2 = 2;
            int BridgeId3 = 3;

            int StatusId1 = 1;
            int StatusId2 = 2;
            int StatusId3 = 3;
            int SeverityId1 = 1;
            int SeverityId2 = 2;
            int SeverityId3 = 3;



            modelBuilder.Entity<Bridge>().HasData(
                new Bridge
                {
                    Id = BridgeId1,
                    BridgeType = "IoT TS",
                    CreatedDate = DateTime.UtcNow,
                    LastModifiedDate = DateTime.UtcNow,
                    CreatedBy = "Initial Migration",
                    LastModifiedBy = "Initial Migration"
                },
                new Bridge
                {
                    Id = BridgeId2,
                    BridgeType = "CMD",
                    CreatedDate = DateTime.UtcNow,
                    LastModifiedDate = DateTime.UtcNow,
                    CreatedBy = "Initial Migration",
                    LastModifiedBy = "Initial Migration"
                },
                new Bridge
                {
                    Id = BridgeId3,
                    BridgeType = "NOC",
                    CreatedDate = DateTime.UtcNow,
                    LastModifiedDate = DateTime.UtcNow,
                    CreatedBy = "Initial Migration",
                    LastModifiedBy = "Initial Migration"
                }
            );


            modelBuilder.Entity<Status>().HasData(
                new Status
                {
                    Id = StatusId1,
                    CurrentStatus = "Active",
                    CreatedDate = DateTime.UtcNow,
                    LastModifiedDate = DateTime.UtcNow,
                    CreatedBy = "Initial Migration",
                    LastModifiedBy = "Initial Migration"
                },
                new Status
                {
                    Id = StatusId2,
                    CurrentStatus = "Inactive",
                    CreatedDate = DateTime.UtcNow,
                    LastModifiedDate = DateTime.UtcNow,
                    CreatedBy = "Initial Migration",
                    LastModifiedBy = "Initial Migration"
                },
                new Status
                {
                    Id = StatusId3,
                    CurrentStatus = "Postponed",
                    CreatedDate = DateTime.UtcNow,
                    LastModifiedDate = DateTime.UtcNow,
                    CreatedBy = "Initial Migration",
                    LastModifiedBy = "Initial Migration"
                }
            );

            modelBuilder.Entity<Severity>().HasData(
                new Severity
                {
                    Id = SeverityId1,
                    IncidentSeverity = "P1",
                    NotificationInterval = 60,
                    CreatedDate = DateTime.UtcNow,
                    LastModifiedDate = DateTime.UtcNow,
                    CreatedBy = "Initial Migration",
                    LastModifiedBy = "Initial Migration"
                },
                new Severity
                {
                    Id = SeverityId2,
                    IncidentSeverity = "P2",
                    NotificationInterval = 90,
                    CreatedDate = DateTime.UtcNow,
                    LastModifiedDate = DateTime.UtcNow,
                    CreatedBy = "Initial Migration",
                    LastModifiedBy = "Initial Migration"
                },
                new Severity
                {
                    Id = SeverityId3,
                    IncidentSeverity = "P3",
                    NotificationInterval = 120,
                    CreatedDate = DateTime.UtcNow,
                    LastModifiedDate = DateTime.UtcNow,
                    CreatedBy = "Initial Migration",
                    LastModifiedBy = "Initial Migration"
                }
            );



            modelBuilder.Entity<ActionStore>().HasData(new ActionStore
            {
                Id = 1,
                Order = 1,
                Description = "Action 1",
                State = NotificationState.INITIAL,
                Repeat = false,
                Interval = 15,
                InitTime = DateTime.UtcNow,
            },
            new ActionStore
            {
                Id = 2,
                Order = 2,
                Description = "Action 2",
                State = NotificationState.INITIAL,
                Repeat = false,
                Interval = 15,
                InitTime = DateTime.UtcNow,
            },
            new ActionStore
            {
                Id = 3,
                Order = 3,
                Description = "Action 3",
                State = NotificationState.INITIAL,
                Repeat = true,
                Interval = 15,
                InitTime = DateTime.UtcNow,
            },
            new ActionStore
            {
                Id = 4,
                Order = 4,
                Description = "Action 4",
                State = NotificationState.INITIAL,
                Repeat = true,
                Interval = 15,
                InitTime = DateTime.UtcNow,
            },
            new ActionStore
            {
                Id = 5,
                Order = 5,
                Description = "Action 5",
                State = NotificationState.INITIAL,
                Repeat = true,
                Interval = 15,
                InitTime = DateTime.UtcNow,
            },
            new ActionStore
            {
                Id = 6,
                Order = 6,
                Description = "Action 6",
                State = NotificationState.INITIAL,

                Repeat = false,
                Interval = 15,
                InitTime = DateTime.UtcNow,
            });

            modelBuilder.Entity<NotificationStore>().HasData(
            new NotificationStore
            {
                Id = 1,
                Type = NotificationType.INITIAL,
                Order = 1,
                Group = NotificationGroup.INTERNAL,
                State = NotificationState.INITIAL,
                Repeat = false,
                 Interval = 15,
            },
            new NotificationStore
            {
                Id = 2,
                Type = NotificationType.UPDATE,
                Order = 2,
                Group = NotificationGroup.INTERNAL,
                State = NotificationState.INITIAL,
                Repeat = true,
                 Interval = 15,
            },
            new NotificationStore
            {
                Id = 3,
                Type = NotificationType.FINAL,
                Order = 3,
                Group = NotificationGroup.INTERNAL,
                State = NotificationState.INITIAL,
                Repeat = false,
                Interval = 15,
            }, new NotificationStore
            {
                Id = 4,
                Type = NotificationType.INITIAL,
                Order = 1,
                Group = NotificationGroup.EXTERNAL,
                State = NotificationState.INITIAL,
                Repeat = false,
                Interval = 15,
            },
            new NotificationStore
            {
                Id = 5,
                Type = NotificationType.UPDATE,
                Order = 2,
                Group = NotificationGroup.EXTERNAL,
                State = NotificationState.INITIAL,
                Repeat = true,
                Interval = 15,
            },
            new NotificationStore
            {
                Id = 6,
                Type = NotificationType.FINAL,
                Order = 3,
                Group = NotificationGroup.EXTERNAL,
                State = NotificationState.INITIAL,
                Repeat = false,
                Interval = 15,
            });
        }
    }
}
