using DiNet.GPipe.Application.Abstractions;
using DiNet.GPipe.SharedKernel;
using Microsoft.Extensions.Options;

namespace DiNet.GPipe.Infrastructure.Building;

public class LocalBuildWorkspace(IOptions<DirectoryWorkspaceOptions> workspace, IGitRepositoryService gitRepository)
    : IBuildWorkspace, IAsyncDisposable, IDisposable
{
    private Queue<string> _queuedPaths = new();

    public async Task Cleanup(CancellationToken ct)
    {
        await Task.Run(() =>
        {
            while (_queuedPaths.TryDequeue(out var c))
            {
                DirectoryExtension.ForceDeleteDirectory(c);
            }
        }, ct);
    }

    public void Dispose()
    {
        while (_queuedPaths.TryDequeue(out var path))
        {
            DirectoryExtension.ForceDeleteDirectory(path);
        }
    }

    public async ValueTask DisposeAsync()
    {
        await Task.Run(() => Dispose());
    }

    public async Task<string> PrepareWorkspaceAsync(string repositoryUrl, string commitHash, CancellationToken cancellationToken)
    {
        var dir = Path.Join(workspace.Value.WorkspaceDirectory, "Isolated", commitHash);

        // Clean directory if messy TODO: Use directories with BuildID!
        DirectoryExtension.ForceDeleteDirectory(dir);

        _queuedPaths.Enqueue(dir);

        await gitRepository.EnsureWorktreeCommit(repositoryUrl, dir, commitHash, cancellationToken);

        return dir;
    }
}

public static class DirectoryExtension
{
    public static void ForceDeleteDirectory(string directory)
    {
        if (!Directory.Exists(directory)) return;

        File.SetAttributes(directory, FileAttributes.Normal);

        string[] files = Directory.GetFiles(directory);
        foreach (var file in files)
        {
            File.SetAttributes(file, FileAttributes.Normal);
            File.Delete(file);
        }

        string[] dirs = Directory.GetDirectories(directory);
        foreach (string dir in dirs)
        {
            ForceDeleteDirectory(dir);
        }

        Directory.Delete(directory, false);
    }
}

