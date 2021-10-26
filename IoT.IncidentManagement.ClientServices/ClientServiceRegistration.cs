using IoT.IncidentManagement.ClientApp.Contracts;
using IoT.IncidentManagement.ClientServices.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;

namespace IoT.IncidentManagement.ClientServices
{
    public static class ClientServiceRegistration
    {
        public static IServiceCollection AddClientServices(this IServiceCollection services, IConfiguration configuration)
        {

            var url = configuration.GetValue<string>("ApiServer:URL");
            services.AddHttpClient<IIncidentClient, IncidentClient>(client => client.BaseAddress = new Uri(url));
            services.AddHttpClient<INoteClient, NoteClient>(client => client.BaseAddress = new Uri(url));
            services.AddHttpClient<IBridgeClient, BridgeClient>(client => client.BaseAddress = new Uri(url));
            services.AddHttpClient<IParticipantClient, ParticipantClient>(client => client.BaseAddress = new Uri(url));
            services.AddHttpClient<IStatusClient, StatusClient>(client => client.BaseAddress = new Uri(url));
            services.AddHttpClient<ISeverityClient, SeverityClient>(client => client.BaseAddress = new Uri(url));
            services.AddHttpClient<INotificationClient, NotificationClient>(client => client.BaseAddress = new Uri(url));
            services.AddHttpClient<IClosureActionClient, ClosureActionClient>(client => client.BaseAddress = new Uri(url));
            services.AddHttpClient<IManagerActionClient, ManagerActionClient>(client => client.BaseAddress = new Uri(url));

            return services;
        }
    }
}