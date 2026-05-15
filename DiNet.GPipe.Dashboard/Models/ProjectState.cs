using DiNet.GPipe.Application.Handlers.Projects.Get;

namespace DiNet.GPipe.Dashboard.Models;

public class ProjectState
{
    public ProjectState(int id, string name, string gitUrl, bool isActive, TimeSpan pollInterval, List<BranchDto> branches)
    {
        Id = id;
        Name = name;
        GitUrl = gitUrl;
        IsActive = isActive;
        PollInterval = pollInterval;
        Branches = branches;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string GitUrl { get; set; }
    public bool IsActive { get; set; }
    public TimeSpan PollInterval { get; set; }
    public List<BranchDto> Branches { get; set; }
}
