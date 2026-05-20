using DiNet.GPipe.SharedKernel.Interfaces.Logging;

namespace DiNet.GPipe.Infrastructure.Building;

public interface IBuildWorkspace : IAsyncDisposable
{
    Task<string> PrepareWorkspaceAsync(string repositoryUrl, string commitHash, CancellationToken cancellationToken);
    Task Cleanup(CancellationToken ct);
}
