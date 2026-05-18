using DiNet.GPipe.Application.Handlers.Abstraction;
using DiNet.GPipe.SharedKernel.Interfaces;
using DiNet.GPipe.SharedKernel.Results;

namespace DiNet.GPipe.Application.Handlers.Projects.Update;

internal class UpdateGitUrlHandler(IProjectService projectService) 
    : ICommandHandler<UpdateGitUrlCommand>
{
    public async Task<Result> Handle(UpdateGitUrlCommand command, CancellationToken ct)
    {
        return await projectService.UpdateGitUrl(command.ProjectId, command.NewGitUrl, ct);
    }
}
