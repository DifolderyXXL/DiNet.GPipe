using DiNet.GPipe.Application.Handlers.Abstraction;

namespace DiNet.GPipe.Application.Handlers.Branches.RemoveByProjectId;

public record RemoveBranchByNameCommand(int ProjectId, string BranchName) : ICommand;
