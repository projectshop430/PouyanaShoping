using Domain.Exceptions;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.SeedData;
using Microsoft.AspNetCore.Mvc;
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
            ApiBehaviorOptions(builder);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
          

            builder.Services.AddSwaggerGen();
            //IHTTPContext
            builder.Services.AddHttpContextAccessor();
            //cache memory
            builder.Services.AddDistributedMemoryCache();
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
        private static void ApiBehaviorOptions(WebApplicationBuilder builder)
        {
            //TODO check this
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(e => e.Value != null && e.Value.Errors.Count > 0)
                        .SelectMany(v => v.Value!.Errors)
                        .Select(c => c.ErrorMessage).ToList();

                    return new BadRequestObjectResult(new ApiToReturn(400, errors));
                };
            });
        }
    }
}
