using DiNet.GPipe.SharedKernel.Interfaces.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DiNet.GPipe.Infrastructure.Messaging;

public class EventBus(IServiceProvider services, ILogger<EventBus> logger) : IEventBus
{
    public async Task PublisthAsync<T>(T @event, CancellationToken ct) where T : class
    {
        var handlers = services.GetServices<IAsyncCommandHandler<T>>().ToList();
            
        if(handlers.Count == 0)
        {
            logger.LogWarning("No handlers registered for event {Type}", typeof(T).Name);

            return;
        }

        var tasks = handlers.Select(async handler =>
        {
            try
            {
                await handler.HandleAsync(@event, ct);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing handler {Handler} for event {Event}",
                    handler.GetType().Name, typeof(T).Name);
            }
        });

        await Task.WhenAll(tasks);
    }
}