namespace DiNet.GPipe.SharedKernel.Interfaces.Messaging;

public interface IEventBus
{
    Task PublisthAsync<T>(T @event, CancellationToken ct) where T : class;
}
