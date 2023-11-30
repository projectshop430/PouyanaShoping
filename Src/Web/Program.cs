using Application;
using Infrastructure;
using Web;
using Web.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
//Configuartion
builder.AddWebServiceCollation();


var app = builder.Build();
app.UseMiddleware<MiddlewareExceptionHandler>();
//access  wwwroot
app.UseStaticFiles();
await app.AddWebAppService().ConfigureAwait(false);