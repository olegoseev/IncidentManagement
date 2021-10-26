using IoT.IncidentManagement.Application.Contracts.Persistence;
using IoT.IncidentManagement.Persistence.Context;
using IoT.IncidentManagement.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IoT.IncidentManagement.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IncidentManagementDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DatasourceConnectionString")));

            services.AddScoped(typeof(IAppRepository<>), typeof(AppRepository<>));
            services.AddScoped<IBridgeRepository, BridgeRepository>();
            services.AddScoped<IClosureActionRepository, ClosureActionRepository>();
            services.AddScoped<IIncidentRepository, IncidentRepository>();
            services.AddScoped<INoteRepository, NoteRepository>();
            services.AddScoped<IManagerActionRepository, ManagerActionRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IParticipantRepository, ParticipantRepository>();
            services.AddScoped<ISeverityRepository, SeverityRepository>();
            services.AddScoped<IStatusRepository, StatusRepository>();
            services.AddScoped<IActionStoreRepository, ActionStoreRepository>();
            services.AddScoped<INotificationStoreRepository, NotificationStoreRepository>();

            return services;
        }
    }
}
