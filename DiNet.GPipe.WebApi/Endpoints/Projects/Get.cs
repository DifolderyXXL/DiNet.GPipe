using DiNet.GPipe.Application.Handlers.Messaging;
using DiNet.GPipe.Application.Handlers.Projects.Get;
using DiNet.GPipe.SharedKernel.Results;
using DiNet.GPipe.WebApi.Infrastructure;

namespace DiNet.GPipe.WebApi.Endpoints.Projects;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/projects", async (
            IQueryHandler<GetProjectsQuery, List<ProjectResponse>> handler,
            CancellationToken ct) =>
        {
            var query = new GetProjectsQuery();

            var result = await handler.Handle(query, ct);

            return result.Match(Results.Ok, CustomResults.Problem);
        });
    }
}
