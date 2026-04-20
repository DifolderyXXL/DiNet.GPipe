namespace DiNet.GPipe.BackgroundWorker.Apk.Consuming;

public interface IPulishApkConsumer
{
    public Task<bool> PublishApk(IApkFile file, CancellationToken cancellationToken = default);
}
