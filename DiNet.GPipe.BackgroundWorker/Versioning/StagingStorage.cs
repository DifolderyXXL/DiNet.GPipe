using DiNet.GPipe.BackgroundWorker.Apk;
using DiNet.GPipe.BackgroundWorker.Common;
using DiNet.GPipe.BackgroundWorker.Versioning;

namespace DiNet.GPipe.BackgroundWorker.Storage;

public class StagingStorage(IStorageProducer store) : IApkStagingStorage
{
    public async Task<IApkFile> Store(IApkFile file, BuildType buildType, string commitHash, CancellationToken cancellationToken)
    {
        var apk = await store.Store(file, "staging_{commitHash}.apk", cancellationToken);

        return apk;
    }
}


