using DiNet.GPipe.Application.Handlers.Messaging;
using DiNet.GPipe.Application.Handlers.Watchers.Deactivate;
using DiNet.GPipe.SharedKernel.Results;
using DiNet.GPipe.WebApi.Infrastructure;

namespace DiNet.GPipe.WebApi.Endpoints.Watchers;

public class ChangeActiveById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/watchers/chengeactive/{id}/{activeState}", async (
            int id,
            bool activeState,
            ICommandHandler<ChangeActiveWatcherCommand> handler,
            CancellationToken ct) =>
        {
            var query = new ChangeActiveWatcherCommand(id, 
                activeState ? ChangeActiveWatcherCommand.ActiveStateCommand.Activate : ChangeActiveWatcherCommand.ActiveStateCommand.Diactivate);

            var result = await handler.Handle(query, ct);

            return result.Match(()=>Results.Ok(), CustomResults.Problem);

        });
    }
}
