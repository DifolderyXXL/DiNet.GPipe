using DiNet.GPipe.Application.Handlers.Abstraction;
using DiNet.GPipe.Application.Handlers.Projects.Update;
using DiNet.GPipe.SharedKernel.Results;
using DiNet.GPipe.WebApi.Infrastructure;

namespace DiNet.GPipe.WebApi.Endpoints.Projects;

public class UpdateGitUrl : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/project/{projectId:int}/giturl", async (
            int projectId,
            UpdateGitUrlRequest request,
            ICommandHandler<UpdateGitUrlCommand> handler,
            CancellationToken ct) =>
        {
            var command = new UpdateGitUrlCommand(projectId, request.NewGitUrl);
            var result = await handler.Handle(command, ct);

            return result.Match(() => Results.Ok(), CustomResults.Problem);
        });
    }
}

public record UpdateGitUrlRequest(string NewGitUrl);
