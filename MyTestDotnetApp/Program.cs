using MyTestDotnetApp.Endpoints;

var builder = WebApplication.CreateBuilder(args);
//This is where we configure this web application with all
//services, middlewear etc.
var app = builder.Build();
app.MapGamesEndpoint();

app.Run();
