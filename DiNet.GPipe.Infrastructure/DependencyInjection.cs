using DiNet.GPipe.Application.Abstractions;
using DiNet.GPipe.Application.Versions;
using DiNet.GPipe.Application.Workers;
using DiNet.GPipe.BuildingApplication.Apk;
using DiNet.GPipe.BuildingApplication.Handlers;
using DiNet.GPipe.BuildingApplication.Infrastructure;
using DiNet.GPipe.Infrastructure.Building;
using DiNet.GPipe.Infrastructure.Database;
using DiNet.GPipe.Infrastructure.DataRepositories;
using DiNet.GPipe.Infrastructure.Git;
using DiNet.GPipe.Infrastructure.Messaging;
using DiNet.GPipe.Infrastructure.Project;
using DiNet.GPipe.Infrastructure.Versions;
using DiNet.GPipe.Infrastructure.Workers;
using DiNet.GPipe.JavaBuilder;
using DiNet.GPipe.JavaBuilder.Settings;
using DiNet.GPipe.SharedKernel;
using DiNet.GPipe.SharedKernel.Interfaces;
using DiNet.GPipe.SharedKernel.Interfaces.Messaging;
using DiNet.GPipe.SharedKernel.Watchers;
using LibGit2Sharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiNet.GPipe.Infrastructure;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddInfrastructure(IConfiguration configuration)
            => services
                .UseEventBus()
                .AddWatcherManagement()
                .UseLocalDirectoryRepository(configuration)
                .UseGit()
                .UseGradlewBuilding()
                .UseLocalIsolatedBuilding(configuration)
                .UseProjectStorage()
                .AddVersions()
                .UseDatabase(configuration);

        IServiceCollection AddVersions()
        {
            services.AddScoped<IVersionService, VersionService>();
            return services;
        }

        IServiceCollection UseEventBus()
        {
            services.AddScoped<IEventBus, EventBus>();

            return services;
        }

        IServiceCollection AddWatcherManagement()
        {
            services.AddSingleton<IProjectWatcherManager, ProjectWatcherManager>();
            services.AddSingleton<IWorkerFactory, WorkerFactory>();

            services.AddSingleton<IProjectService, ProjectService>();

            services.AddScoped<IWatcherOrchestrator, WatcherOrchestrator>();

            return services;
        }

        IServiceCollection UseLocalDirectoryRepository(IConfiguration configuration)
        {
            services.AddScoped(typeof(IDataRepository<>), typeof(ScopedFileDataRepository<>));


            var section = configuration.GetSection(nameof(ScopedStorageOptions));
            if (!section.Exists())
                throw new Exception("ScopedStorageOptions is not configured!");
            services.AddOptions<ScopedStorageOptions>()
                .Bind(section);

            var sectionJdk = configuration.GetSection(nameof(JdkSettings));
            if (!sectionJdk.Exists())
                throw new Exception("JdkSettings is not configured!");

            services.AddOptions<JdkSettings>()
                .Bind(sectionJdk);



            var releaseSection = configuration.GetSection(nameof(SignedReleaseBuildOptions));
            if (!releaseSection.Exists())
                throw new Exception("SignedReleaseBuildOptions is not configured!");
            services.AddOptions<SignedReleaseBuildOptions>()
                .Bind(releaseSection);


            return services;
        }

        IServiceCollection UseProjectStorage()
        {
            services.AddScoped<IApkProjectStorage, ApkProjectStorage>();

            return services;
        }

        IServiceCollection UseGit()
        {
            services.AddScoped<IGitRepositoryService, LibGit2RepositoryService>();

            services.AddScoped<ICommitSource, ScopedCommitSource>();

            services.AddScoped<IProjectScopeContext, ProjectScopeContext>();

            return services;
        }

        IServiceCollection UseDatabase(IConfiguration configuration)
        {
            var connectionString =
                configuration.GetConnectionString("sqlite")
                    ?? throw new InvalidOperationException("Connection string"
                    + "'sqlite' not found.");

            services.AddDbContextPool<AppDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });

            services.AddScoped<IProjectsRepository, ProjectsRepository>();
            services.AddScoped<IBuildRegistryRepository, BuildRegistryRepository>();

            services.AddScoped<IActivityRepository, ActivityRepository>();
            services.AddScoped<ICommitsRepository, CommitsRepository>();

            return services;
        }

        IServiceCollection UseGradlewBuilding()
        {
            services.AddScoped<IApkBuilder, GradleApkBuilder>();
            return services;
        }

        IServiceCollection UseLocalIsolatedBuilding(IConfiguration configuration) 
        {

            var workspaceSection = configuration.GetSection(nameof(DirectoryWorkspaceOptions));
            if (!workspaceSection.Exists())
                throw new Exception("DirectoryWorkspaceOptions is not configured!");
            services.AddOptions<DirectoryWorkspaceOptions>()
                .Bind(workspaceSection);

            services.AddScoped<IIsolatedBuilder, IsolatedSpaceBuilder>();
            services.AddScoped<IBuildWorkspace, LocalBuildWorkspace>();

            return services;
        }
    }
}
