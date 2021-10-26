
using MediatR;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using System.Reflection;
using IoT.IncidentManagement.Contstants;

namespace IoT.IncidentManagement.Application
{
    public static class ApplicationServiceRegistration
    {
        /// <summary>
        /// extention method to register AutoMapper adn MediatR
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            ApplicationConstants.ActionChangeInterval = configuration.GetValue<int>("Application:ActionChangeInterval");

            return services;
        }
    }
}
