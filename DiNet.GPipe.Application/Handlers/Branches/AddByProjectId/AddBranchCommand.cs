using DiNet.GPipe.Application.Handlers.Abstraction;
using DiNet.GPipe.SharedKernel.Watchers;

namespace DiNet.GPipe.Application.Handlers.Branches.AddByProjectId;

public record AddBranchCommand(int ProjectId, BranchConfig Branch) : ICommand;
