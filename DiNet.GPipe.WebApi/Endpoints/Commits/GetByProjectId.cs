using DiNet.GPipe.Application.Handlers.Abstraction;
using DiNet.GPipe.Application.Handlers.Projects.Get;
using DiNet.GPipe.WebApi.Infrastructure;
using DiNet.GPipe.SharedKernel.Results;
using DiNet.GPipe.Application.Handlers.Commits.Get;

namespace DiNet.GPipe.WebApi.Endpoints.Commits;

public class GetByProjectId : IEndpoint
{
    public class Request
    {
        public int ProjectId { get; set; }
        public bool IncludeActivity { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/project-commits", async (
            [AsParameters] Request request,
            IQueryHandler <GetCommitsByProjectIdQuery, List<CommitResponse>> handler,
            CancellationToken ct) =>
        {
            var query = new GetCommitsByProjectIdQuery(request.ProjectId, request.IncludeActivity);

            var result = await handler.Handle(query, ct);

            return result.Match(Results.Ok, CustomResults.Problem);
        });
    }
}
