using DiNet.GPipe.Application.Handlers.Messaging;

namespace DiNet.GPipe.Application.Handlers.Watchers.GetById;

public sealed record GetWatcherByIdQuery(Guid Id) : IQuery<WatcherResponse>;
