namespace DiNet.GPipe.BuildingApplication.Apk.Consuming;

public interface IPublication
{
    public Task<bool> PublishStreamAsync(Stream stream, CancellationToken cancellationToken = default);
}
