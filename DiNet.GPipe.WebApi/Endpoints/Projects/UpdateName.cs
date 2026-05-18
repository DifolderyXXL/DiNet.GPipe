using DiNet.GPipe.Application.Handlers.Abstraction;
using DiNet.GPipe.Application.Handlers.Projects.Update;
using DiNet.GPipe.SharedKernel.Results;
using DiNet.GPipe.WebApi.Infrastructure;

namespace DiNet.GPipe.WebApi.Endpoints.Projects;

public class UpdateName : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/project/{projectId:int}/name", async (
            int projectId,
            UpdateNameRequest request,
            ICommandHandler<UpdateProjectNameCommand> handler,
            CancellationToken ct) =>
        {
            var command = new UpdateProjectNameCommand(projectId, request.NewName);
            var result = await handler.Handle(command, ct);

            return result.Match(() => Results.Ok(), CustomResults.Problem);
        });
    }
}

public record UpdateNameRequest(string NewName);
