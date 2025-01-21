using Portfolio_Backend.Extensions;
using Portfolio_Backend.Middleware;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddLoggingServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();


app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://127.0.0.1:4200", "http://localhost:4200", "https://thomashaley.app"));


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();

