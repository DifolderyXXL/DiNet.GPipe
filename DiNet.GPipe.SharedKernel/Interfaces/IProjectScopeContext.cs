namespace DiNet.GPipe.Infrastructure.Project;

public interface IProjectScopeContext
{
    public int ProjectId { get; }
    public string ProjectName { get; }
    public string GitUrl { get;}

    public void Initialize(int projectId, string projectName, string gitUrl);
}
