using DiNet.GPipe.Application.Handlers.Abstraction;
using DiNet.GPipe.Application.Service;
using DiNet.GPipe.SharedKernel.Interfaces.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace DiNet.GPipe.Application;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddApplication()
        => services
            .AddCommandHandlers()
            .AddEventHandlers()
            .AddServices();


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

        IServiceCollection AddEventHandlers()
        {
            services.Scan(scan => scan.FromAssembliesOf(typeof(DependencyInjection))
                .AddClasses(classes => classes.AssignableTo(typeof(IAsyncEventHandler<>)), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );

            return services;
        }

        IServiceCollection AddServices()
        {
            services.AddScoped<ProjectService>();

            return services;
        }
    }
}
