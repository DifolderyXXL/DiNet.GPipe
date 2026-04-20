using DiNet.GPipe.BackgroundWorker.Apk;
using DiNet.GPipe.BackgroundWorker.Storage.Storing;

namespace DiNet.GPipe.BackgroundWorker.Storage;

public interface IDistributedStorage
{
    public Task<IApkFile> Move(IApkFile file, StorageAreaType area, CancellationToken cancellationToken);
    public Task<IApkFile> Move(IApkFile file, string targetName, StorageAreaType area, CancellationToken cancellationToken);
    public IEnumerable<IApkFile> EnumerateAll(StorageAreaType area);
}


public class DistributedStorage(string baseDirectory) : IDistributedStorage
{
    private string GetStorageAreaDirectory(StorageAreaType area) => Path.Join(baseDirectory, area.ToString());

    public async Task<IApkFile> Move(IApkFile file, StorageAreaType area, CancellationToken cancellationToken)
    {
        return await Move(file, Path.GetFileName(file.FilePath), area, cancellationToken);
    }

    public async Task<IApkFile> Move(IApkFile file, string targetName, StorageAreaType area, CancellationToken cancellationToken)
    {
        var fileDirectory = GetStorageAreaDirectory(area);
        
        Directory.CreateDirectory(fileDirectory);

        var target = Path.Join(fileDirectory, targetName);

        await file.MoveToAsync(target, cancellationToken);

        return file;
    }

    public IEnumerable<IApkFile> EnumerateAll(StorageAreaType area)
    {
        var fileDirectory = GetStorageAreaDirectory(area);

        return Directory.EnumerateFiles(fileDirectory).Select(x=>new SystemApkFile(x));
    }
}


