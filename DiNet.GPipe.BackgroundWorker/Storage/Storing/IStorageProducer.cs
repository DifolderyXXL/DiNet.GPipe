using DiNet.GPipe.BackgroundWorker.Apk;

namespace DiNet.GPipe.BackgroundWorker.Storage;

public interface IStorageProducer
{
    public Task<IApkFile> Store(IApkFile file, string targetName, CancellationToken cancellationToken);
}
