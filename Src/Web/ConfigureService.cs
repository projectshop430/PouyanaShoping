using Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Web
{
    public static class ConfigureService
    {
        public static IServiceCollection AddWebServiceCollation(this WebApplicationBuilder Builder)
        {
            Builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Builder.Configuration.GetConnectionString("DefaultConnection")));
            return Builder.Services;
        }
    }
}
