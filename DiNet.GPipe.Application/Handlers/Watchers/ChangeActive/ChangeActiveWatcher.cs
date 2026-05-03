using DiNet.GPipe.Application.Handlers.Messaging;
using DiNet.GPipe.Application.Workers;
using DiNet.GPipe.SharedKernel.Results;
using static DiNet.GPipe.Application.Handlers.Watchers.Deactivate.ChangeActiveWatcherCommand;

namespace DiNet.GPipe.Application.Handlers.Watchers.Deactivate;

public record ChangeActiveWatcherCommand(int ProjectId, ActiveStateCommand ActiveState) : ICommand
{
    public enum ActiveStateCommand
    {
        Activate,
        Diactivate
    }
}
public class ChangeActiveWatcher(IWatcherOrchestrator watcherOrchestrator) : ICommandHandler<ChangeActiveWatcherCommand>
{
    public async Task<Result> Handle(ChangeActiveWatcherCommand command, CancellationToken ct)
    {
        return await watcherOrchestrator.ChangeActiveAsync(
            command.ProjectId,  
            command.ActiveState == ActiveStateCommand.Activate,
            ct);
    }
}
