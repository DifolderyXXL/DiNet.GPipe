using System;
using DiNet.GPipe.Application.Handlers.Abstraction;
using DiNet.GPipe.Infrastructure.Building.V2;
using DiNet.GPipe.SharedKernel.Results;

namespace DiNet.GPipe.Application.Handlers.Builds.GetActive;

public class GetActiveBuildsQueryHandler(IActiveBuildTracker tracker) : IQueryHandler<GetActiveBuildsQuery, List<ActiveBuildResponse>>
{
    public async Task<Result<List<ActiveBuildResponse>>> Handle(GetActiveBuildsQuery query, CancellationToken ct)
    {
        return tracker.GetActiveBuilds()
            .Select(x => new ActiveBuildResponse(x.BuildId, x.ProjectId, x.ProjectName, x.State, x.StartedAt))
            .ToList();
    }
}
