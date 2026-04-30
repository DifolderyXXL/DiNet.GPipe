using DiNet.GPipe.Domain;
using DiNet.GPipe.SharedKernel.Watchers;

namespace DiNet.GPipe.SharedKernel.Models;

public record WatcherParameters(
    ProjectModel Project,
    List<BranchConfig> Branches,
    TimeSpan Period
    );



