using System.Collections.Concurrent;
using DiNet.GPipe.SharedKernel.Interfaces.Logging;

namespace DiNet.GPipe.Infrastructure.Building.V2;

public class InMemoryActiveBuildTracker : IActiveBuildTracker
{
    private readonly ConcurrentDictionary<string, (ActiveBuild Build, IProcessLogger Logger)> _active = new();

    public void TrackActive(string buildId, int projectId, string projectName, IProcessLogger logger)
    {
        var activeBuild = new ActiveBuild(buildId, projectId, projectName, BuilderState.Running, DateTime.UtcNow);
        _active.TryAdd(buildId, (activeBuild, logger));
    }

    public void UpdateState(string buildId, BuilderState state)
    {
        if (_active.TryGetValue(buildId, out var tuple))
        {
            _active[buildId] = (tuple.Build with { State = state }, tuple.Logger);
        }
    }

    public void RemoveActive(string buildId)
    {
        _active.TryRemove(buildId, out _);
    }

    public IReadOnlyCollection<ActiveBuild> GetActiveBuilds()
        => _active.Values.Select(v => v.Build).ToList();

    public IProcessLogger? GetLogger(string buildId)
        => _active.TryGetValue(buildId, out var tuple) ? tuple.Logger : null;

    public bool IsProjectBuilding(int projectId)
        => _active.Values.Any(v => v.Build.ProjectId == projectId && v.Build.State == BuilderState.Running);
}
