// namespace API;
using Portfolio_Backend.Extensions;
using Portfolio_Backend.Middleware;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddLoggingServices(builder.Configuration);
// builder.Services.AddIdentityServices (builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://127.0.0.1:4200", "https://thomashaley.app"));


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();

