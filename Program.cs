using api.Data;
using api.Data.AutoloadSingleton;
using api.Data.ScreenServices;
using api.Data.ScreenServices.Interfaces;
using api.SignalR.ScreenHub;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();

builder.Services.AddDbContext<AppDbContext>(ServiceLifetime.Singleton);

builder.Services.AddSingleton<IOnlineService, OnlineService>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "Cors",
        policy  =>
        {
            policy.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader();
        });
});

var app = builder.Build();

ConfigureService.Configure(app);

app.UseCors("Cors");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapPost("broadcast", async (string message, IHubContext<ScreenHub, IScreenHub> context) => {
    await context.Clients.All.RecieveMessage(message);
    return Results.NoContent();
});

app.MapHub<ScreenHub>("/screen-hub");

app.MapControllers();

app.Run();
