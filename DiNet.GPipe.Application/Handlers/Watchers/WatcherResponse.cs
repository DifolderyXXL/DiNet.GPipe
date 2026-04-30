using DiNet.GPipe.SharedKernel.Watchers;

namespace DiNet.GPipe.Application.Handlers.Watchers;

public sealed record WatcherResponse(
    int ProjectId,
    string ProjectName,
    string GitUrl,
    List<BranchConfig> Branches,
    WatcherStatus Status
    );