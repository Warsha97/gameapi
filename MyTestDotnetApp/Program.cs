using MyTestDotnetApp.Data;
using MyTestDotnetApp.Endpoints;

var builder = WebApplication.CreateBuilder(args);
//This is where we configure this web application with all
//services, middlewear etc.
// test
var connString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connString);

var app = builder.Build();
app.MapGamesEndpoint();
app.MigrateDB();

app.Run();
