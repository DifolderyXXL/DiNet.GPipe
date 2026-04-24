using DiNet.GPipe.BackgroundWorker.Common;

namespace DiNet.GPipe.BackgroundWorker.Build;


public enum BuildStatus
{
    Success,
    Failed,
}
public class BuildRecord
{
    public int Id { get; set; }
    public BuildVersion? Version { get; set; }
    public string CommitHash { get; set; }
    public string BuildPath { get; set; }
    public BuildStatus Status { get; set; }
    public string? Error { get; set; }
}

public interface IBuildRepository
{
    public Task Save(BuildRecord build);

    public Task CreateFailedRecord(BuildVersion? version, string commitHash, string error);
}