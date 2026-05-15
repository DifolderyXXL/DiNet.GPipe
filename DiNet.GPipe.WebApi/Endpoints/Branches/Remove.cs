using DiNet.GPipe.Application.Handlers.Abstraction;
using DiNet.GPipe.Application.Handlers.Branches.RemoveByProjectId;
using DiNet.GPipe.SharedKernel.Results;
using DiNet.GPipe.WebApi.Infrastructure;

namespace DiNet.GPipe.WebApi.Endpoints.Branches;

public class Remove : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("project/{projectId}/branch/remove", async (
            int projectId,
            string branchName,
            ICommandHandler<RemoveBranchByNameCommand> handler,
            CancellationToken ct) =>
        {
            var command = new RemoveBranchByNameCommand(projectId, branchName);

            var result = await handler.Handle(command, ct);

            return result.Match(() => Results.Ok(), CustomResults.Problem);
        });
    }
}
