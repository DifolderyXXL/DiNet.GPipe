using DiNet.GPipe.SharedKernel.Results;
using DiNet.GPipe.SharedKernel.Watchers;

namespace DiNet.GPipe.Application.Workers;

public interface IWatcherOrchestrator
{
    public Task InitializeAsync(CancellationToken ct);
    public Task<Result> ChangeActiveAsync(int projectId, bool active, CancellationToken ct);
    public Task<Result<int>> CreateAndStartWatcher(WatcherRequest request, CancellationToken ct);
}
