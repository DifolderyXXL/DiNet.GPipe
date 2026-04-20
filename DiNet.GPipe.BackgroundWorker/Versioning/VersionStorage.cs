using DiNet.GPipe.BackgroundWorker.Apk;
using DiNet.GPipe.BackgroundWorker.Common;
using DiNet.GPipe.BackgroundWorker.Versioning;

namespace DiNet.GPipe.BackgroundWorker.Storage;

public class VersionStorage(BranchVersion versionType,
                            IVersionService versionService,
                            IStorageProducer store,
                            IMetadataNamingService namingService) : IApkVersionStorage
{
    public async Task<IApkFile> Store(IApkFile file, BuildType buildType, CancellationToken cancellationToken)
    {
        var version = versionService.Put(versionType);

        var metadata = new ApkMetadata(version, buildType);
        var name = namingService.GetName(metadata);

        var apk = await store.Store(file, name, cancellationToken);

        return apk;
    }
}


