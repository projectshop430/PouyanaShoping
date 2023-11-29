using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.SeedData;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Web
{
    public static class ConfigureService
    {
        public static IServiceCollection AddWebServiceCollation(this WebApplicationBuilder builder)
        {
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
          

            builder.Services.AddSwaggerGen();
            return builder.Services;
        }
        public static async Task<IApplicationBuilder> AddWebAppService(this WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            //get service
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var context = services.GetRequiredService<ApplicationDbContext>();
            try
            {

                await context.Database.MigrateAsync();
                await GenerateFakeData.SeedDataAsync(context, loggerFactory);
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(e, "error exception for migrations");
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
            return app;
        }
    }
}
