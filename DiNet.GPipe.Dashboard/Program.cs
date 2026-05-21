using DiNet.GPipe.Dashboard.Api;
using DiNet.GPipe.Dashboard.Components;
using DiNet.GPipe.Dashboard.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Refit;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped(typeof(IState<>), typeof(GenericState<>));

builder.Services
    .AddRefitClient<IProjectApi>()
    .AddRefitClient<IBuildingApi>()
    .AddRefitClient<IWatcherApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https+http://dinet-gpipe-webapi"));
builder.Services.AddHttpClient("WebApiServer", c => c.BaseAddress = new("http://dinet-gpipe-webapi"))
    .AddServiceDiscovery();

builder.Services.AddTransient(sp =>
{
    var handler = sp.GetRequiredService<IHttpMessageHandlerFactory>().CreateHandler("WebApiServer");

    return new HubConnectionBuilder()
        .WithUrl("http://dinet-gpipe-webapi/buildlog", options => options.HttpMessageHandlerFactory = _ => handler)
        .WithAutomaticReconnect()
        .Build();
});



builder.Services.AddBlazorBootstrap();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
