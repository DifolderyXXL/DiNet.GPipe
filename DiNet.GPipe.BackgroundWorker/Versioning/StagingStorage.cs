using DiNet.GPipe.BackgroundWorker.Apk;
using DiNet.GPipe.BackgroundWorker.Versioning;
using DiNet.GPipe.SharedKernel.Models;

namespace DiNet.GPipe.BackgroundWorker.Storage;

public class StagingStorage(IStorageProducer store) : IApkStagingStorage
{
    public async Task<IApkFile> Store(IApkFile file, BuildType buildType, string commitHash, CancellationToken cancellationToken)
    {
        var apk = await store.Store(file, "staging_{commitHash}.apk", cancellationToken);

        return apk;
    }
}


