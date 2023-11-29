using Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Web
{
    public static class ConfigureService
    {
        public static IServiceCollection AddWebServiceCollation(this WebApplicationBuilder Builder)
        {
            return Builder.Services;
        }
    }
}
