namespace DiNet.GPipe.Domain;

public class BuildRegistry
{
    public string CommitHash { get; set; }
    public int ProjectId { get; set; }
    public string Version { get; set; }
    public DateTime CommitDate { get; set; }
    public BuildStatus Status { get; set; }
}

public enum BuildStatus
{
    Pending,
    Building, 
    Success,
    Failed
}

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; }
}