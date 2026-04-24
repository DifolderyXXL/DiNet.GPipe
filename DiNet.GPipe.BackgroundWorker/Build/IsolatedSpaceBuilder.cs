using DiNet.GPipe.BackgroundWorker.Apk;
using DiNet.GPipe.BackgroundWorker.Common;
using DiNet.GPipe.BackgroundWorker.Git;
using DiNet.GPipe.SharedKernel;

namespace DiNet.GPipe.BackgroundWorker.Build;



public record struct BuildCommand(string branch, string commitHash);
public class IsolatedSpaceBuilder(IApkBuilder api, IGitRepositoryService gitRepository)
{
    public async Task<Result<IApkFile>> BuildIsolated(string workingDirectory, BuildCommand buildCommand, CancellationToken cancellation)
    {
        await gitRepository.EnsureWorktreeCommit(workingDirectory, buildCommand.commitHash, cancellation);

        var result = await api.Provide(new ApkProvideCommand(workingDirectory, BuildType.Release));

        return result;
    }
}