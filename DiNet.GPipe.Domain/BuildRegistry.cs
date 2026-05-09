namespace DiNet.GPipe.Domain;

public class BuildRegistry
{
    public string CommitHash { get; set; }
    public int ProjectId { get; set; }
    public ProjectModel Project { get; set; }
    public BuildVersion Version { get; set; }
    public DateTime CommitDate { get; set; }
    public BuildStatus Status { get; set; }
}
