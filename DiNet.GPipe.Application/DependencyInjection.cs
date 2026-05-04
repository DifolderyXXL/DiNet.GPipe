using DiNet.GPipe.Application.Project;
using DiNet.GPipe.Application.Versions;
using DiNet.GPipe.Application.Workers;
using DiNet.GPipe.SharedKernel.Watchers;
using Microsoft.Extensions.DependencyInjection;
using DiNet.GPipe.Application.Handlers.Abstraction;

namespace DiNet.GPipe.Application;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddApplication()
        => services
            .AddCommandHandlers()
            .AddVersions();

        IServiceCollection AddVersions()
        {
            services.AddScoped<IVersionService, VersionService>();
            return services;
        }

        IServiceCollection AddCommandHandlers()
        {
            services.Scan(scan => scan.FromAssembliesOf(typeof(DependencyInjection))
                .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)), publicOnly: false)
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()

                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)), publicOnly: false)
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()

                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)), publicOnly: false)
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
                );

            return services;
        }

    }
}
