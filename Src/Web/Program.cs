using Application;
using Infrastructure;
using Web;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
//Configuartion
builder.AddWebServiceCollation();


var app = builder.Build();
await app.AddWebAppService().ConfigureAwait(false);