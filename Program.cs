
using hot_demo.extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

app.UseAuthentication();

app.UseWebSockets();

app.MapGraphQL();

app.Run();
