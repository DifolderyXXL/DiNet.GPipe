namespace DiNet.GPipe.BackgroundWorker.Build;

public class DirectoryWorkspaceOptions
{
    public string WorkspaceDirectory { get; set; } = null!;
    public string WorkingDirectory => Path.Join(WorkspaceDirectory, "Working");
    public string ProjectsApkDirectory => Path.Join(WorkspaceDirectory, "ProjectsApk");
}
