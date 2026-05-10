using DiNet.GPipe.Application.Handlers.Abstraction;
using DiNet.GPipe.Application.Project;
using DiNet.GPipe.Application.Workers;
using DiNet.GPipe.SharedKernel.Extensions;
using DiNet.GPipe.SharedKernel.Interfaces;
using DiNet.GPipe.SharedKernel.Results;
using DiNet.GPipe.SharedKernel.Watchers;
using Microsoft.Extensions.Logging;

namespace DiNet.GPipe.Application.Handlers.Projects.DeleteById;
internal class DeleteProject(IProjectsRepository repository, IProjectWatcherManager watchers, ILogger<DeleteProject> logger) : ICommandHandler<DeleteProjectByIdCommand>
{
    public async Task<Result> Handle(DeleteProjectByIdCommand command, CancellationToken ct)
    {
        try
        {
            await repository.Delete(command.ProjectId);
            await watchers.DeleteWatcherAsync(command.ProjectId, ct);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "DeleteProject error");
            return ProjectErrors.ProjectNotFounded().AsResult();
        }

        return Result.Success();
    }
}
