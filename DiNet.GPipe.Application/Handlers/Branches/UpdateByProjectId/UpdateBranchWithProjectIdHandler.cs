using DiNet.GPipe.Application.Handlers.Abstraction;
using DiNet.GPipe.Application.Service;
using DiNet.GPipe.SharedKernel.Results;
using DiNet.GPipe.SharedKernel.Watchers;

namespace DiNet.GPipe.Application.Handlers.Branches.UpdateByProjectId;

public record UpdateBranchWithProjectIdCommand(int ProjectId, string BranchName, BranchConfig newBranch) : ICommand;
internal class UpdateBranchWithProjectIdHandler(ProjectService service) : ICommandHandler<UpdateBranchWithProjectIdCommand>
{
    public async Task<Result> Handle(UpdateBranchWithProjectIdCommand command, CancellationToken ct)
    {
        var result = await service.UpdateBranch(command.ProjectId, command.BranchName, command.newBranch);

        return result ? Result.Success() : Result.Failure(new Error("Update failed.", ErrorType.Problem));
    }
}
