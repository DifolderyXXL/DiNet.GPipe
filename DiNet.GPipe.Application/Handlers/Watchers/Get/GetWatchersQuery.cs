using DiNet.GPipe.Application.Handlers.Abstraction;

namespace DiNet.GPipe.Application.Handlers.Watchers.Get;

public sealed record GetWatchersQuery : IQuery<List<WatcherResponse>>;
