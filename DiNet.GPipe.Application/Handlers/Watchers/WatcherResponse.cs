using DiNet.GPipe.SharedKernel.Watchers;

namespace DiNet.GPipe.Application.Handlers.Watchers;

public sealed record WatcherResponse(
    int ProjectId,
    string ProjectName,
    string GitUrl,
    IReadOnlyList<BranchConfig> Branches,
    WatcherStatus Status
    );