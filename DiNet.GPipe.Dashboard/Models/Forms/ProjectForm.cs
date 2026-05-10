using DiNet.GPipe.Domain;

namespace DiNet.GPipe.Dashboard.Models.Forms;

public class BranchForm
{
    public string BranchName { get; set; } = "";
    public BranchVersion VersionType { get; set; }
}

public class ProjectForm
{
    public string Name { get; set; } = "";
    public string GitUrl { get; set; } = "";
    public bool FastStart { get; set; }
    public TimeSpan PollInterval { get; set; } = TimeSpan.FromMinutes(5);
    public List<BranchForm> BranchConfigs { get; set; } = new();
}