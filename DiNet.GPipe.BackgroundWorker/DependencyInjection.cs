using DiNet.GPipe.BackgroundWorker.Apk;
using DiNet.GPipe.BackgroundWorker.Build;
using DiNet.GPipe.BackgroundWorker.Git;
using DiNet.GPipe.BuildingApplication.Handlers;
using DiNet.GPipe.BuildingApplication.Infrastructure;
using DiNet.GPipe.JavaBuilder;
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


            var section = configuration.GetSection(nameof(DirectoryWorkspaceOptions));
            if (!section.Exists())
                throw new Exception("DirectoryWorkspaceOptions is not configured!");
            services.AddOptions<DirectoryWorkspaceOptions>()
                .Bind(section);


            var releaseSection = configuration.GetSection(nameof(SignedReleaseBuildOptions));
            if (!section.Exists())
                throw new Exception("SignedReleaseBuildOptions is not configured!");
            services.AddOptions<SignedReleaseBuildOptions>()
                .Bind(releaseSection);


            services.AddScoped<IsolatedSpaceBuilder>();
            services.AddScoped<IApkBuilder, GradleApkBuilder>();
            services.AddScoped<IGitRepositoryService, GitRepositoryService>();
            services.AddScoped<IApkProjectStorage, ApkProjectStorage>();

            return services;
        }
    }
}
