using DiNet.GPipe.Application.Handlers.Abstraction;
using DiNet.GPipe.Application.Service;
using DiNet.GPipe.SharedKernel.Results;

namespace DiNet.GPipe.Application.Handlers.Branches.AddByProjectId;
internal class AddByProjectIdHandler(ProjectService service) : ICommandHandler<AddBranchCommand>
{
    public async Task<Result> Handle(AddBranchCommand command, CancellationToken ct)
    {
        var result = await service.AddBranch(command.ProjectId, command.Branch);

        return result 
            ? Result.Success() 
            : Result.Failure(new Error("Can't add new branch: " + command.Branch, ErrorType.Problem));
    }
}
