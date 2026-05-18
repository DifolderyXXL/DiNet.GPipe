using DiNet.GPipe.Domain;
using DiNet.GPipe.SharedKernel.Watchers;

namespace DiNet.GPipe.SharedKernel.Models;

public record WatcherParameters(
    int ProjectId,
    ProjectWatcherConfig Config
);

public record ProjectWatcherConfig(
    string ProjectName,
    string GitUrl,
    IReadOnlyList<BranchConfig> Branches,
    TimeSpan PollInterval
);