using DiNet.GPipe.Application.Handlers.Messaging;
using DiNet.GPipe.SharedKernel.Extensions;
using DiNet.GPipe.SharedKernel.Results;
using DiNet.GPipe.SharedKernel.Watchers;

namespace DiNet.GPipe.Application.Handlers.Watchers.GetById;
internal class GetWatcherById(IWatcherManager manager) : IQueryHandler<GetWatcherByIdQuery, WatcherResponse>
{
    public async Task<Result<WatcherResponse>> Handle(GetWatcherByIdQuery query, CancellationToken ct)
    {
        var watcher = await manager.GetWatcherAsync(query.Id, ct);

        return watcher switch
        {
            null => WatcherErrors.WatcherNotFounded().AsResult<WatcherResponse>(),
            _ => Result.Success(
                new WatcherResponse(watcher.Id, watcher.ProjectName, watcher.GitUrl, watcher.Branches, watcher.Status))
        };
    }
}
