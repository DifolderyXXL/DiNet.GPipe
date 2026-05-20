using System;
using DiNet.GPipe.Application.Handlers.Abstraction;
using DiNet.GPipe.Application.Project;
using DiNet.GPipe.Infrastructure.Building.V2;
using DiNet.GPipe.SharedKernel.Interfaces;
using DiNet.GPipe.SharedKernel.Results;

namespace DiNet.GPipe.Application.Handlers.Builds.Enqueue;

public record EnqueueBuildCommand(int ProjectId, string CommitHash) : ICommand;
public class EnqueueBuildCommandHandler(IBuildQueue buildQueue, IProjectsRepository projectsRepository) : ICommandHandler<EnqueueBuildCommand>
{
    public async Task<Result> Handle(EnqueueBuildCommand command, CancellationToken ct)
    {
        var project = await projectsRepository.Get(command.ProjectId);

        if (project == null)
        {
            return ProjectErrors.ProjectNotFound();
        }

        var commit = project.Commits.FirstOrDefault(x => x.Hash == command.CommitHash);
        if (commit == null)
        {
            return new Error("Commit not found.", ErrorType.NotFound);
        }

        await buildQueue.QueueBuildAsync(new BuildRequest(
            command.ProjectId,
            project.GitUrl,
            command.CommitHash,
            project.Name,
            commit.Branch
        ));

        return Result.Success();
    }
}
