using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;



namespace Application
{
    public static class ConfigureService
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
         
            //collection add => service provider get =>DI
            services.AddMediatR(Assembly.GetExecutingAssembly());
           
        }
    }
}
