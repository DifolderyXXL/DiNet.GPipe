using DiNet.GPipe.SharedKernel.Models;
using DiNet.GPipe.SharedKernel.Results;
using DiNet.GPipe.BuildingApplication.Apk;
using DiNet.GPipe.BuildingApplication.Git;

namespace DiNet.GPipe.BuildingApplication.Build;


public class IsolatedSpaceBuilder(IApkBuilder api, IGitRepositoryService gitRepository) : IAsyncDisposable
{
    private Queue<string> _queuedPaths = new();

    public async Task<Result<IApkFile>> BuildIsolated(string repositoryUrl, string workingDirectory, string commitHash, CancellationToken cancellation)
    {
        var dir = Path.Combine(workingDirectory, "Isolated", commitHash);

        _queuedPaths.Enqueue(dir);

        await gitRepository.EnsureWorktreeCommit(repositoryUrl, dir, commitHash, cancellation);

        var result = await api.Build(new ApkBuildCommand(dir, BuildType.Release));

        return result;
    }

    public async Task Cleanup(CancellationToken ct)
    {
        await Task.Run(() =>
        {
            while (_queuedPaths.TryDequeue(out var c))
            {
                Directory.Delete(c, true);
            }
        }, ct);
    }

    public async ValueTask DisposeAsync()
    {
        await Cleanup(default);
    }
}
