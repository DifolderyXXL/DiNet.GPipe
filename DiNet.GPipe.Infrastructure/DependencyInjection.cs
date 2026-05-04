using DiNet.GPipe.Application.Abstractions;
using DiNet.GPipe.Application.Workers;
using DiNet.GPipe.Infrastructure.Database;
using DiNet.GPipe.Infrastructure.DataRepositories;
using DiNet.GPipe.Infrastructure.Git;
using DiNet.GPipe.Infrastructure.Messaging;
using DiNet.GPipe.Infrastructure.Project;
using DiNet.GPipe.Infrastructure.Workers;
using DiNet.GPipe.JavaBuilder.Settings;
using DiNet.GPipe.SharedKernel.Interfaces;
using DiNet.GPipe.SharedKernel.Interfaces.Messaging;
using DiNet.GPipe.SharedKernel.Watchers;
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
                .UseDatabase(configuration);

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
            if (!sectionJdk.Exists()) // Use sectionJdk
                throw new Exception("JdkSettings is not configured!");

            services.AddOptions<JdkSettings>()
                .Bind(sectionJdk);


            return services;
        }

        IServiceCollection UseGit()
        {
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

            return services;
        }
    }
}
