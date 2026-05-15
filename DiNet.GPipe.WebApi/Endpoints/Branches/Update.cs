using DiNet.GPipe.Application.Handlers.Abstraction;
using DiNet.GPipe.Application.Handlers.Branches.UpdateByProjectId;
using DiNet.GPipe.SharedKernel.Results;
using DiNet.GPipe.SharedKernel.Watchers;
using DiNet.GPipe.WebApi.Infrastructure;

namespace DiNet.GPipe.WebApi.Endpoints.Branches;

public class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("project/{projectId}/branch/{branchName}/update", async (
            int projectId,
            string branchName,
            BranchConfig branch,
            ICommandHandler<UpdateBranchWithProjectIdCommand> handler,
            CancellationToken ct) =>
        {
            var command = new UpdateBranchWithProjectIdCommand(projectId, branchName, branch);

            var result = await handler.Handle(command, ct);

            return result.Match(() => Results.Ok(), CustomResults.Problem);
        });
    }
}
