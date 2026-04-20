using DiNet.GPipe.BackgroundWorker.Apk;
using DiNet.GPipe.BackgroundWorker.Common;
using DiNet.GPipe.BackgroundWorker.Versioning;

namespace DiNet.GPipe.BackgroundWorker.Branches;

public class BranchTrigger(IApkVersionStorage storage, string branchName)
{
    public readonly string BranchName = branchName;

    public async Task Trigger()
    {
        IApkProviderApi api = null!;

        var apk = await api.Provide(new ApkProvideCommand(BuildType.Release));

        await storage.Store(apk, BuildType.Release, default);
    }
}


