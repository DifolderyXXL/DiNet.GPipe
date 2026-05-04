using DiNet.GPipe.SharedKernel.Watchers;
using Microsoft.Extensions.DependencyInjection;

namespace DiNet.GPipe.Application.Workers;

public record WatcherEntry(
    CancellationTokenSource CancellationTokenSource,
    Watcher Watcher,
    IServiceScope Scope,
    BranchCheckWorker Instance);
