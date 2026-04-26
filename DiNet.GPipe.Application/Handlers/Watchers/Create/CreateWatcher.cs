using DiNet.GPipe.Application.Handlers.Messaging;
using DiNet.GPipe.SharedKernel.Results;
using DiNet.GPipe.SharedKernel.Watchers;

namespace DiNet.GPipe.Application.Handlers.Watchers.Create;
internal class CreateWatcher(IWatcherManager manager) : ICommandHandler<CreateWatcherCommand, CreatedWatcherResponse>
{
    public async Task<Result<CreatedWatcherResponse>> Handle(CreateWatcherCommand command, CancellationToken ct)
    {
        var watcherId = await manager.CreateWatcherAsync(
            new WatcherRequest(
                command.ProjectName,
                command.GitUrl,
                command.Branches,
                command.PollInterval
                ),
            ct
            );

        return Result.Success(new CreatedWatcherResponse(watcherId));
    }
}