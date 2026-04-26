using DiNet.GPipe.SharedKernel.Watchers;

namespace DiNet.GPipe.Application.Handlers.Watchers;

public sealed record WatcherResponse(
    Guid Id,
    string ProjectName,
    string GitUrl,
    List<BranchConfig> Branches,
    WatcherStatus Status
    );