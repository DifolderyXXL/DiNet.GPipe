using DiNet.GPipe.SharedKernel.Interfaces.Logging;

namespace DiNet.GPipe.Infrastructure.Building.V2;

public interface IActiveBuildTracker
{
    void TrackActive(string buildId, int projectId, string projectName, IProcessLogger logger);
    IProcessLogger? GetLogger(string buildId);
    void UpdateState(string buildId, BuilderState state);
    void RemoveActive(string buildId);
    IReadOnlyCollection<ActiveBuild> GetActiveBuilds();
    bool IsProjectBuilding(int projectId);
}
