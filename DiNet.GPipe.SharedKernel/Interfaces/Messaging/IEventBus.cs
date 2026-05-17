namespace DiNet.GPipe.SharedKernel.Interfaces.Messaging;

public interface IEventBus
{
    Task PublishAsync<T>(T @event, CancellationToken ct) where T : class;
}
