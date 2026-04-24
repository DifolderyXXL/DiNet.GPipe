using DiNet.GPipe.BackgroundWorker;
using DiNet.GPipe.BackgroundWorker.Git;


var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();


builder.Services.ConfigureOptions(new GitRepositoryService(new(@"https://github.com/DifolderyXXL/DiNet.RctSchedule.Widget.git")));
builder.Services.AddScoped<IGitRepositoryService, GitRepositoryService>();


var host = builder.Build();
host.Run();