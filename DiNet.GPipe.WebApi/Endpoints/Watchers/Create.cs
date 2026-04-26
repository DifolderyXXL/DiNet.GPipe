using DiNet.GPipe.Application.Handlers.Messaging;
using DiNet.GPipe.Application.Handlers.Watchers.Create;
using DiNet.GPipe.SharedKernel.Results;
using DiNet.GPipe.WebApi.Infrastructure;

namespace DiNet.GPipe.WebApi.Endpoints.Watchers;

public class Create : IEndpoint
{
    //private class BranchRequest
    //{

    //}
    //private class Request
    //{
    //    string ProjectName { get; set; }
    //    string GitUrl { get; set; }
    //    List<BranchConfig> Branches { get; set; }
    //    TimeSpan PollInterval { get; set; }
    //}

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/watcher", async (
            CreateWatcherCommand command,
            ICommandHandler <CreateWatcherCommand, CreatedWatcherResponse> handler,
            CancellationToken ct) =>
        {
            var result = await handler.Handle(command, ct);

            return result.Match(Results.Ok, CustomResults.Problem);

        });
    }
}
