using DiNet.GPipe.Application;
using DiNet.GPipe.Application.Workers;
using DiNet.GPipe.Infrastructure;
using DiNet.GPipe.Infrastructure.Database;
using DiNet.GPipe.WebApi.Extensions;
using DiNet.GPipe.WebApi.Hubs;
using DiNet.GPipe.WebApi.Infrastructure;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddServiceDefaults();

builder.Services.AddHostedService<BuildQueueProcessor>();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddEndpoints(typeof(Program).Assembly);


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

var app = builder.Build();

app.MapHub<BuildLogHub>("/buildlog");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapSwagger();

app.MapDefaultEndpoints();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    var strategy = db.Database.CreateExecutionStrategy();
    await strategy.ExecuteAsync(async () =>
    {
        await db.Database.MigrateAsync();
    });
}

using (var scope = app.Services.CreateScope())
{
    var orchestration = scope.ServiceProvider.GetRequiredService<IWatcherOrchestrator>();

    await orchestration.InitializeAsync(default);
}

app.MapEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();