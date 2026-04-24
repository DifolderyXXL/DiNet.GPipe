namespace DiNet.GPipe.BackgroundWorker.Build;

public interface IWorkingDirectoryWorkspace
{
    public string WorkingDirectory { get; }
}
public class LocalWorkingDirectoryWorkspace(string directory) : IWorkingDirectoryWorkspace
{
    public string WorkingDirectory => directory;
}