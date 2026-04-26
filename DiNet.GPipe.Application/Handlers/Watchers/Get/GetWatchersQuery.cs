using DiNet.GPipe.Application.Handlers.Messaging;

namespace DiNet.GPipe.Application.Handlers.Watchers.GetByUrl;

public sealed record GetWatchersQuery : IQuery<List<WatcherResponse>>;
