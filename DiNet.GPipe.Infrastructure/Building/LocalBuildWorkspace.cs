using DiNet.GPipe.Application.Abstractions;
using DiNet.GPipe.SharedKernel;
using Microsoft.Extensions.Options;

namespace DiNet.GPipe.Infrastructure.Building;

public class LocalBuildWorkspace(IOptions<DirectoryWorkspaceOptions> workspace, IGitRepositoryService gitRepository)
    : IBuildWorkspace, IAsyncDisposable
{
    private Queue<string> _queuedPaths = new();

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

    public async Task<string> PrepareWorkspaceAsync(string repositoryUrl, string commitHash, CancellationToken cancellationToken)
    {
        var dir = Path.Combine(workspace.Value.WorkspaceDirectory, "Isolated", commitHash);
        _queuedPaths.Enqueue(dir);

        await gitRepository.EnsureWorktreeCommit(repositoryUrl, dir, commitHash, cancellationToken);

        return dir;
    }
}
