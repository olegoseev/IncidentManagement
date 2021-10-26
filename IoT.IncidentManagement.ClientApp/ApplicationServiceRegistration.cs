
using MediatR;

using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

namespace IoT.IncidentManagement.ClientApp
{
    public static class ApplicationServiceRegistration
    {
        /// <summary>
        /// extention method to register AutoMapper adn MediatR
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
