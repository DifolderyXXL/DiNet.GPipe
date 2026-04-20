namespace DiNet.GPipe.BackgroundWorker.Apk.Consuming;

public interface IPublication
{
    public Task<bool> PublishStreamAsync(Stream stream, CancellationToken cancellationToken = default);
}
