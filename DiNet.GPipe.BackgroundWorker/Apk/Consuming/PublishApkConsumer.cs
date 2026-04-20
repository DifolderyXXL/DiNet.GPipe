using DiNet.GPipe.BackgroundWorker.Storage;
using DiNet.GPipe.BackgroundWorker.Storage.Storing;

namespace DiNet.GPipe.BackgroundWorker.Apk.Consuming;

public class PublishApkConsumer(IPublication publication, IDistributedStorage areaStorage) : IPulishApkConsumer
{
    public async Task<bool> PublishApk(IApkFile file, CancellationToken cancellationToken = default)
    {
        using var fs = file.OpenStream();

        var result = await publication.PublishStreamAsync(fs, cancellationToken);
        await fs.DisposeAsync();
        
        if (result)
        {
            await areaStorage.Move(file, StorageAreaType.Published, cancellationToken);
        }

        return result;
        
    }
}
