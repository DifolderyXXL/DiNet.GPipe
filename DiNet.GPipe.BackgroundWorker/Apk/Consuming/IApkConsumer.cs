namespace DiNet.GPipe.BackgroundWorker.Apk.Consuming;

public interface IApkConsumer
{
    public Task Consume(IApkFile apk, CancellationToken cancellationToken = default);
}
