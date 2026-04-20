using DiNet.GPipe.BackgroundWorker.Apk;
using DiNet.GPipe.BackgroundWorker.Common;

namespace DiNet.GPipe.BackgroundWorker.Versioning;

public interface IApkVersionStorage
{
    public Task<IApkFile> Store(IApkFile file, BuildType buildType, CancellationToken cancellationToken);
}


