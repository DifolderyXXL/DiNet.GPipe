using DiNet.GPipe.Application.Handlers.Abstraction;
using DiNet.GPipe.Application.Handlers.Branches.AddByProjectId;
using DiNet.GPipe.Domain;
using DiNet.GPipe.SharedKernel.Results;
using DiNet.GPipe.SharedKernel.Watchers;
using DiNet.GPipe.WebApi.Infrastructure;

namespace DiNet.GPipe.WebApi.Endpoints.Branches;

public class Add : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("project/{projectId}/branch/add", async (
            int projectId,
            BranchConfig branch,
            ICommandHandler<AddBranchCommand> handler,
            CancellationToken ct) =>
        {
            var command = new AddBranchCommand(projectId, branch);

            var result = await handler.Handle(command, ct);

            return result.Match(()=>Results.Ok(), CustomResults.Problem);
        });
    }
}
