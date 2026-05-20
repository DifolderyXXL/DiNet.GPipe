using System.Threading.Channels;

namespace DiNet.GPipe.Infrastructure.Building.V2;

public class ChannelBuildQueue : IBuildQueue
{
    private readonly Channel<BuildRequest> _queue = Channel.CreateBounded<BuildRequest>(new BoundedChannelOptions(100)
    {
        FullMode = BoundedChannelFullMode.Wait
    });

    public IAsyncEnumerable<BuildRequest> ReaderAllAsync(CancellationToken cancellationToken)
        => _queue.Reader.ReadAllAsync(cancellationToken);

    public ValueTask QueueBuildAsync(BuildRequest request)
        => _queue.Writer.WriteAsync(request);
}
