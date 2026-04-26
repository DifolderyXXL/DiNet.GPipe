using DiNet.GPipe.SharedKernel.Results;

namespace DiNet.GPipe.Application.Handlers.Watchers;

public static class WatcherErrors
{
    public static Error WatcherNotFounded() => new Error(nameof(WatcherNotFounded), ErrorType.Internal);
}
