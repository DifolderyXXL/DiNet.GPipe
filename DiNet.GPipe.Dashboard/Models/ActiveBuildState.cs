using DiNet.GPipe.Infrastructure.Building.V2;

namespace DiNet.GPipe.Dashboard.Models;

public class ActiveBuildState
{
    public ActiveBuildState(string buildId, int projectId, string projectName, BuilderState state, DateTime startedAt)
    {
        BuildId = buildId;
        ProjectId = projectId;
        ProjectName = projectName;
        State = state;
        StartedAt = startedAt;
    }

    public string BuildId { get; set; }
    public int ProjectId { get; set; }
    public string ProjectName { get; set; }
    public BuilderState State { get; set; }
    public DateTime StartedAt { get; set; }
}
