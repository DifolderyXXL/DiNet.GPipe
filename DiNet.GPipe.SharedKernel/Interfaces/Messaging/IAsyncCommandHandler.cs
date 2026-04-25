namespace DiNet.GPipe.SharedKernel.Interfaces.Messaging;

public interface IAsyncCommandHandler<T>
{
    public Task HandleAsync(T command, CancellationToken ct);
}