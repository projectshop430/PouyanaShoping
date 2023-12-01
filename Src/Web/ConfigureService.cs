using Domain.Exceptions;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.SeedData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Web.Middleware;

namespace Web
{
    public static class ConfigureService
    {
        public static IServiceCollection AddWebServiceCollation(this WebApplicationBuilder builder , IConfiguration configuration)
        {
            // Add services to the container.

            builder.Services.AddControllers();
            ApiBehaviorOptions(builder);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
          

            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy",
                    policy =>
                    {
                        policy.AllowAnyHeader().AllowAnyMethod()
                            .WithOrigins(configuration["CorsAddress:AddressHttp"] ?? string.Empty,
                                configuration["CorsAddress:AddressHttps"] ?? string.Empty);
                    });
            });

            //IHTTPContext
            builder.Services.AddHttpContextAccessor();
            //cache memory
            builder.Services.AddDistributedMemoryCache();
            return builder.Services;
        }
        public static async Task<IApplicationBuilder> AddWebAppService(this WebApplication app)
        {
            app.UseMiddleware<MiddlewareExceptionHandler>();
            //access  wwwroot
          
            var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            //get service
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var context = services.GetRequiredService<ApplicationDbContext>();
            try
            {

              //  await context.Database.MigrateAsync();
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
            app.UseStaticFiles();
            app.UseRouting();
            //CORS
            app.UseCors("CorsPolicy");
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
