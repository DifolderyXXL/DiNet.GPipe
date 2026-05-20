using DiNet.GPipe.Infrastructure.Building.V2;

namespace DiNet.GPipe.Application.Handlers.Builds.GetActive;

public record ActiveBuildResponse(
    string BuildId,
    int ProjectId,
    string ProjectName,
    BuilderState State,
    DateTime StartedAt
);
