namespace DiNet.GPipe.SharedKernel.Interfaces.Messaging;

public interface IAsyncEventHandler<T>
{
    public Task HandleAsync(T command, CancellationToken ct);
}