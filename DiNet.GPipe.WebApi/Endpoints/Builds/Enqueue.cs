using DiNet.GPipe.Application.Handlers.Abstraction;
using DiNet.GPipe.Application.Handlers.Builds.Enqueue;
using DiNet.GPipe.Application.Handlers.Builds.GetActive;
using DiNet.GPipe.Infrastructure.Building.V2;
using DiNet.GPipe.SharedKernel.Results;
using DiNet.GPipe.WebApi.Extensions;

namespace DiNet.GPipe.WebApi.Endpoints.Builds;

public class Enqueue : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("builds/enqueue", async (int projectId, string commitHash,
            ICommandHandler<EnqueueBuildCommand> handler,
            CancellationToken ct) =>
        {
            var request = new EnqueueBuildCommand(projectId, commitHash);
            var result = await handler.Handle(request, ct);
            return result.ToWebResult();
        });
    }
}

public class GetActive : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("builds/active", async (
            IQueryHandler<GetActiveBuildsQuery, List<ActiveBuildResponse>> handler,
            CancellationToken ct) =>
        {
            var request = new GetActiveBuildsQuery();
            var result = await handler.Handle(request, ct);
            return result.ToWebResult();
        });
    }
}

public class GetLogs : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("builds/logs", async (string builderId, int maxLines,
            IActiveBuildTracker tracker,
            CancellationToken ct) =>
        {
            var logger = tracker.GetLogger(builderId);

            if (logger == null)
            {
                return Result.Failure(new Error("Cant find builder", ErrorType.NotFound)).ToWebResult();
            }

            return Result.Success(logger.GetRecentLogs(maxLines)).ToWebResult();
        });
    }
}