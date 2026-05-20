namespace DiNet.GPipe.Infrastructure.Building.V2;

public record ActiveBuild(
    string BuildId,
    int ProjectId,
    string ProjectName,
    BuilderState State,
    DateTime StartedAt);
