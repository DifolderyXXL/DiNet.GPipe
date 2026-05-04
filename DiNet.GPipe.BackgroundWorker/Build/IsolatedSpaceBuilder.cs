using DiNet.GPipe.BackgroundWorker.Apk;
using DiNet.GPipe.BackgroundWorker.Git;
using DiNet.GPipe.SharedKernel.Models;
using DiNet.GPipe.SharedKernel.Results;

namespace DiNet.GPipe.BackgroundWorker.Build;


public class IsolatedSpaceBuilder(IApkBuilder api, IGitRepositoryService gitRepository)
{
    public async Task<Result<IApkFile>> BuildIsolated(string repositoryUrl, string workingDirectory, string commitHash, CancellationToken cancellation)
    {
        var dir = Path.Combine(workingDirectory, "Isolated");

        await gitRepository.EnsureWorktreeCommit(repositoryUrl, dir, commitHash, cancellation);

        var result = await api.Build(new ApkBuildCommand(dir, BuildType.Release));

        return result;
    }
}
