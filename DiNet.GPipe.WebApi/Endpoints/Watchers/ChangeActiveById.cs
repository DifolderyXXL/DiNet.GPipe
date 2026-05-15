using DiNet.GPipe.SharedKernel.Results;
using DiNet.GPipe.WebApi.Infrastructure;
using DiNet.GPipe.Application.Handlers.Abstraction;
using DiNet.GPipe.Application.Handlers.Watchers.ChangeActive;

namespace DiNet.GPipe.WebApi.Endpoints.Watchers;

public class ChangeActiveById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/watchers/{id}/chengeactive", 
        async (
            int id,
            bool activeState,
            ICommandHandler<ChangeActiveWatcherCommand> handler,
            CancellationToken ct) =>
        {
            var query = new ChangeActiveWatcherCommand(id, 
                activeState ? ActiveStateCommand.Activate: ActiveStateCommand.Diactivate);

            var result = await handler.Handle(query, ct);

            return result.Match(()=>Results.Ok(), CustomResults.Problem);

        });
    }
}
