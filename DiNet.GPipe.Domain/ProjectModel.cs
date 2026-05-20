namespace DiNet.GPipe.Domain;

public class ProjectModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string GitUrl { get; set; }

    public WatcherSettings WatcherSettings { get; set; }

    public List<BranchWatcherConfig> BranchConfigs { get; set; } = [];

    public List<CommitEntry> Commits { get; set; } = [];
    public List<BuildRegistry> Builds { get; set; } = [];
}


public class WatcherSettings
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public bool IsActive { get; set; }
    public DateTime? LastGlobalCheck { get; set; }
    public TimeSpan PollInterval { get; set; }
}

public class BranchWatcherConfig
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public string BranchName { get; set; }
    public BranchVersion VersionType { get; set; }
}