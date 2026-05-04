using DiNet.GPipe.SharedKernel.Watchers;
using DiNet.GPipe.Application.Handlers.Abstraction;

namespace DiNet.GPipe.Application.Handlers.Watchers.Create;

public sealed record CreateWatcherCommand(string ProjectName, string GitUrl, bool FastStart, List<BranchConfig> Branches, TimeSpan PollInterval) : ICommand<CreatedWatcherResponse>;
