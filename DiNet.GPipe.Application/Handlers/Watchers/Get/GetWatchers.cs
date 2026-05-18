using DiNet.GPipe.SharedKernel.Results;
using DiNet.GPipe.SharedKernel.Watchers;
using DiNet.GPipe.Application.Handlers.Abstraction;
using DiNet.GPipe.Application.Handlers.Watchers.Get;

namespace DiNet.GPipe.Application.Handlers.Watchers.Get;

internal class GetWatchers(IProjectWatcherManager manager) : IQueryHandler<GetWatchersQuery, List<WatcherResponse>>
{
    public async Task<Result<List<WatcherResponse>>> Handle(GetWatchersQuery query, CancellationToken ct)
    {
        var watchers = manager.EnumerateAllWatchers();

        return Result.Success(
            watchers.Select(
                watcher => new WatcherResponse(
                    watcher.ProjectId, watcher.Config.ProjectName, watcher.Config.GitUrl, watcher.Config.Branches, watcher.Status))
            .ToList()
            );

    }
}
