using Application;
using Infrastructure;
using Web;
using Web.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
//Configuartion
builder.AddWebServiceCollation(builder.Configuration);


//comit
var app = builder.Build();

await app.AddWebAppService().ConfigureAwait(false);