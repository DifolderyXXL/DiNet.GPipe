using DiNet.GPipe.SharedKernel.Results;
using DiNet.GPipe.WebApi.Infrastructure;
using DiNet.GPipe.Application.Handlers.Abstraction;
using DiNet.GPipe.Application.Handlers.Projects.DeleteById;

namespace DiNet.GPipe.WebApi.Endpoints.Projects;

public class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/project", async (
            int ProjectId,
            ICommandHandler<DeleteProjectByIdCommand> handler,
            CancellationToken ct) =>
        {
            var command = new DeleteProjectByIdCommand(ProjectId);

            var result = await handler.Handle(command, ct);

            return result.Match(()=>Results.Ok(), CustomResults.Problem);
        });
    }
}
