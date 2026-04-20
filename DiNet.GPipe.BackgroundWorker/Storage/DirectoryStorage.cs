using DiNet.GPipe.BackgroundWorker.Apk;
using DiNet.GPipe.BackgroundWorker.Storage.Storing;

namespace DiNet.GPipe.BackgroundWorker.Storage;

public class TargetAreaStorage(IDistributedStorage storage, StorageAreaType area) : IStorageProducer
{
    public async Task<IApkFile> Store(IApkFile file, string targetName, CancellationToken cancellationToken)
    {
        return await storage.Move(file, targetName, area, cancellationToken);
    }

    public IEnumerable<IApkFile> EnumerateAll()
    {
        return storage.EnumerateAll(area);
    }
}


