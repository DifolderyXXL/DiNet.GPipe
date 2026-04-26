namespace DiNet.GPipe.Infrastructure.Project;

public class ProjectScopeContext : IProjectScopeContext
{
    public int ProjectId { get; private set; }

    public string ProjectName { get; private set; }

    public string GitUrl { get; private set; }

    public void Initialize(int projectId, string projectName, string gitUrl)
    {
        ProjectId = projectId;
        ProjectName = projectName;
        GitUrl = gitUrl;
    }
}
