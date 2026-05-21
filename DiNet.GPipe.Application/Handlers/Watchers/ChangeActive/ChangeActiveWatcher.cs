using DiNet.GPipe.Application.Workers;
using DiNet.GPipe.SharedKernel.Results;
using DiNet.GPipe.Application.Handlers.Abstraction;

namespace DiNet.GPipe.Application.Handlers.Watchers.ChangeActive;


public enum ActiveStateCommand
{
    Activate,
    Diactivate

}

public record ChangeActiveWatcherCommand(int ProjectId, ActiveStateCommand ActiveState) : ICommand;
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
