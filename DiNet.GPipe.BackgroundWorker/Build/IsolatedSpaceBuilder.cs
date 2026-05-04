using DiNet.GPipe.BackgroundWorker.Apk;
using DiNet.GPipe.BackgroundWorker.Git;
using DiNet.GPipe.SharedKernel.Models;
using DiNet.GPipe.SharedKernel.Results;

namespace DiNet.GPipe.BackgroundWorker.Build;


public class IsolatedSpaceBuilder(IApkBuilder api, IGitRepositoryService gitRepository)
{
    public async Task<Result<IApkFile>> BuildIsolated(string repositoryUrl, string workingDirectory, string commitHash, CancellationToken cancellation)
    {
        await gitRepository.EnsureWorktreeCommit(repositoryUrl, workingDirectory, commitHash, cancellation);

        var result = await api.Build(new ApkBuildCommand(workingDirectory, BuildType.Release));

        return result;
    }
}