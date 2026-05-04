using DiNet.GPipe.BackgroundWorker.Apk;
using DiNet.GPipe.BackgroundWorker.Build;
using DiNet.GPipe.BackgroundWorker.Git;
using DiNet.GPipe.BuildingApplication.Infrastructure;
using DiNet.GPipe.SharedKernel.Interfaces.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiNet.GPipe.BuildingApplication;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddBuildingApplication(IConfiguration configuration)
        {
            services.Scan(scan => scan.FromAssembliesOf(typeof(DependencyInjection))
                .AddClasses(classes => classes.AssignableTo(typeof(IAsyncEventHandler<>)), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );


            var section = configuration.GetSection(nameof(LocalDirectoryWorkspaceOptions));
            if (!section.Exists())
                throw new Exception("LocalBuildingDirectoryWorkspaceOptions is not configured!");
            services.AddOptions<LocalDirectoryWorkspaceOptions>()
                .Bind(section);


            services.AddSingleton<IsolatedSpaceBuilder>();
            services.AddSingleton<IApkBuilder, GradleApkBuilder>();
            services.AddSingleton<IGitRepositoryService, GitRepositoryService>();

            return services;
        }
    }
}
