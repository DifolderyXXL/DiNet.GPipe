using DiNet.GPipe.SharedKernel.Interfaces;
using DiNet.GPipe.SharedKernel.Interfaces.Messaging;
using DiNet.GPipe.SharedKernel.Models.Commands;

namespace DiNet.GPipe.Application.EventHandlers;

internal class CommitDetectedHandler(ICommitsRepository repository) : IAsyncEventHandler<CommitDetected>
{
    public async Task HandleAsync(CommitDetected command, CancellationToken ct)
    {
        await repository.Add(new()
        {
            ProjectId = command.ProjectId,
            Name = command.CommitName,
            Hash = command.CommitHash,
            Date = command.CommitDate,
            BuildVersion = command.Version,
        });
    }
}
