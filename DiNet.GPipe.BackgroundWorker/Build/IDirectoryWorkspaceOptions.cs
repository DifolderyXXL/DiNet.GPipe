namespace DiNet.GPipe.BackgroundWorker.Build;

public interface IDirectoryWorkspaceOptions
{
    public string WorkingDirectory { get; }
    public string ProjectsApkDirectory { get; }
}
public class LocalDirectoryWorkspaceOptions : IDirectoryWorkspaceOptions
{
    public string WorkspaceDirectory { get; set; } = null!;
    public string WorkingDirectory => Path.Join(WorkspaceDirectory, "Working");
    public string ProjectsApkDirectory => Path.Join(WorkspaceDirectory, "ProjectsApk");
}