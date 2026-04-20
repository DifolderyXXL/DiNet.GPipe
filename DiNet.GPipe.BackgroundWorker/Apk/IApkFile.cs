namespace DiNet.GPipe.BackgroundWorker.Apk;

public interface IApkFile 
{
    public string FilePath { get; }

    public Stream OpenStream();

    public Task MoveToAsync(string path, CancellationToken cancellationToken);
    public Task RenameAsync(string name, CancellationToken cancellationToken);
}

public class SystemApkFile(string c_path) : IApkFile
{
    public string FilePath { get; private set; } = c_path;

    public Task MoveToAsync(string path, CancellationToken cancellationToken)
    {
        File.Move(this.FilePath, path);
        FilePath = path;

        return Task.CompletedTask;
    }

    public Stream OpenStream()
    {
        return new FileStream(FilePath, FileMode.Open);
    }

    public Task RenameAsync(string name, CancellationToken cancellationToken)
    {
        var path = Path.Join(Path.GetDirectoryName(FilePath), name);
        File.Move(FilePath, path);
        FilePath = path;

        return Task.CompletedTask;
    }
}