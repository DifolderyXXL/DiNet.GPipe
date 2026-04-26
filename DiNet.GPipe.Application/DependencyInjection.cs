using DiNet.GPipe.Application.Project;
using DiNet.GPipe.Application.Versions;
using DiNet.GPipe.Application.Workers;
using DiNet.GPipe.SharedKernel.Interfaces;
using DiNet.GPipe.SharedKernel.Watchers;
using Microsoft.Extensions.DependencyInjection;

namespace DiNet.GPipe.Application;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddApplication()
        => services
            .AddWatcherManager()
            .AddBuilding()
            .AddProjectServices();

        IServiceCollection AddWatcherManager()
        {
            services.AddSingleton<IWatcherManager, WatcherManager>();
            services.AddScoped<IWorkerFactory, WorkerFactory>();

            return services;
        }

        IServiceCollection AddProjectServices()
        {
            services.AddSingleton<IProjectService, ProjectService>();

            return services;
        }


        IServiceCollection AddBuilding()
        {
            services.AddScoped<IVersionService, VersionService>();
            return services;
        }
    }
}
