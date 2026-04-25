using DiNet.GPipe.BackgroundWorker.Branches;
using DiNet.GPipe.Infrastructure.DataRepositories;
using DiNet.GPipe.Infrastructure.Messaging;
using DiNet.GPipe.SharedKernel.Interfaces.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace DiNet.GPipe.Infrastructure;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddInfrastructure()
            => services
                .UseEventBus()
                .UseLocalDirectoryRepository();

        IServiceCollection UseEventBus()
        {
            services.AddScoped<IEventBus, EventBus>();

            return services;
        }

        IServiceCollection UseLocalDirectoryRepository()
        {
            services.AddScoped(typeof(IDataRepository<>), typeof(FileDataRepository<>));

            return services;
        }
    }
}
