using DiNet.GPipe.Application.Handlers.Messaging;
using DiNet.GPipe.Application.Workers;
using DiNet.GPipe.SharedKernel.Results;
using DiNet.GPipe.SharedKernel.Watchers;

namespace DiNet.GPipe.Application.Handlers.Watchers.Create;
internal class CreateWatcher(IWatcherOrchestrator watcherOrchestrator) : ICommandHandler<CreateWatcherCommand, CreatedWatcherResponse>
{
    public async Task<Result<CreatedWatcherResponse>> Handle(CreateWatcherCommand command, CancellationToken ct)
    {
        var watcherId = await watcherOrchestrator.CreateAndStartWatcher(
            new WatcherRequest(
                command.ProjectName,
                command.GitUrl,
                command.Branches,
                command.PollInterval
                ),
            ct
            );



        return watcherId.MatchG(x => new CreatedWatcherResponse(watcherId.Value), e => default);
    }
}