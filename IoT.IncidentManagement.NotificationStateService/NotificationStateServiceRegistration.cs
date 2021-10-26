using IoT.IncidentManagement.NotificationStateService.Services.NotificationMachine;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IoT.IncidentManagement.NotificationStateService
{
    public static class NotificationStateServiceRegistration
    {
        /// <summary>
        /// extention method to register AutoMapper adn MediatR
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddNotificationServices(this IServiceCollection services)
        {
            //services.AddSingleton<IMachineDispatcher, MachineDispatcher>();
            services.AddSingleton<IRxMachine, RxMachine>();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            //services.AddSingleton<TestMachine>(x => ActivatorUtilities.CreateInstance<TestMachine>());
            return services;
        }
    }
}
