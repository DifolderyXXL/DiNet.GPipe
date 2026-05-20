namespace DiNet.GPipe.Infrastructure.Building.V2;

public interface IBuildQueue
{
    public ValueTask QueueBuildAsync(BuildRequest request);
    public IAsyncEnumerable<BuildRequest> ReaderAllAsync(CancellationToken cancellationToken);
}