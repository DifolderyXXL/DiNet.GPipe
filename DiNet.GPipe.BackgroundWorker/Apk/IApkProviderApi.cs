namespace DiNet.GPipe.BackgroundWorker.Apk;

public interface IApkProviderApi
{
    public Task<IApkFile> Provide(ApkProvideCommand command, CancellationToken cancellationToken = default);
}


