using DiNet.GPipe.Application.Handlers.Abstraction;
using DiNet.GPipe.SharedKernel.Interfaces;
using DiNet.GPipe.SharedKernel.Results;

namespace DiNet.GPipe.Application.Handlers.Projects.Update;

internal class UpdateProjectNameHandler(IProjectService projectService) 
    : ICommandHandler<UpdateProjectNameCommand>
{
    public async Task<Result> Handle(UpdateProjectNameCommand command, CancellationToken ct)
    {
        return await projectService.UpdateProjectName(command.ProjectId, command.NewName, ct);
    }
}
