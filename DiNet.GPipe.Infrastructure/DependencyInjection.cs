using DiNet.GPipe.BackgroundWorker.Branches;
using DiNet.GPipe.Infrastructure.Database;
using DiNet.GPipe.Infrastructure.DataRepositories;
using DiNet.GPipe.Infrastructure.Git;
using DiNet.GPipe.Infrastructure.Messaging;
using DiNet.GPipe.Infrastructure.Project;
using DiNet.GPipe.SharedKernel.Interfaces;
using DiNet.GPipe.SharedKernel.Interfaces.Messaging;
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
                .UseLocalDirectoryRepository(configuration)
                .UseGit()
                .UseDatabase(configuration);

        IServiceCollection UseEventBus()
        {
            services.AddScoped<IEventBus, EventBus>();

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
