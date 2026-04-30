using DiNet.GPipe.Application.Handlers.Messaging;
using DiNet.GPipe.SharedKernel.Extensions;
using DiNet.GPipe.SharedKernel.Results;
using DiNet.GPipe.SharedKernel.Watchers;

namespace DiNet.GPipe.Application.Handlers.Watchers.GetByUrl;
internal class GetWatchers(IProjectWatcherManager manager) : IQueryHandler<GetWatchersQuery, List<WatcherResponse>>
{
    public async Task<Result<List<WatcherResponse>>> Handle(GetWatchersQuery query, CancellationToken ct)
    {
        var watchers = manager.EnumerateAllWatchers();

        return Result.Success(
            watchers.Select(
                watcher => new WatcherResponse(watcher.ProjectId, watcher.ProjectName, watcher.GitUrl, watcher.Branches, watcher.Status))
            .ToList()
            );
               
    }
}