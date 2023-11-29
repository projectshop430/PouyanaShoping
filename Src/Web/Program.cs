using Infrastructure;
using Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddSwaggerGen();
//Configuartion
builder.AddWebServiceCollation();
var app = builder.Build();
var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
try
{
    var context = app.Services.GetRequiredService<ApplicationDbContext>();
    await context.Database.MigrateAsync();
   
}
catch(Exception e)
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
