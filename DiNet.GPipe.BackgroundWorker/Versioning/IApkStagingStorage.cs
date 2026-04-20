using DiNet.GPipe.BackgroundWorker.Apk;
using DiNet.GPipe.BackgroundWorker.Common;

namespace DiNet.GPipe.BackgroundWorker.Versioning;

public interface IApkStagingStorage
{
    public Task<IApkFile> Store(IApkFile file, BuildType buildType, string commitHash, CancellationToken cancellationToken);
}


