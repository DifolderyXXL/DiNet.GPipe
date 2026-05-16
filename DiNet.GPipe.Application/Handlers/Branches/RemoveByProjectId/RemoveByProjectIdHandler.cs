using DiNet.GPipe.Application.Handlers.Abstraction;
using DiNet.GPipe.Application.Service;
using DiNet.GPipe.SharedKernel.Results;

namespace DiNet.GPipe.Application.Handlers.Branches.RemoveByProjectId;
internal class RemoveByProjectIdHandler(BranchManagementService service) : ICommandHandler<RemoveBranchByNameCommand>
{
    public async Task<Result> Handle(RemoveBranchByNameCommand command, CancellationToken ct)
    {
        var result = await service.RemoveBranch(command.ProjectId, command.BranchName);

        return result;
    }
}
