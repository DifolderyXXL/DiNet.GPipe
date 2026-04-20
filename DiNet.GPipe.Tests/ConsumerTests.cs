using DiNet.GPipe.BackgroundWorker.Apk;
using DiNet.GPipe.BackgroundWorker.Apk.Consuming;
using DiNet.GPipe.BackgroundWorker.Storage;
using DiNet.GPipe.BackgroundWorker.Storage.Storing;

namespace DiNet.GPipe.Tests;

public class ConsumerTests
{
    private string TestingDirectory = Path.Join(Directory.GetCurrentDirectory(), "Testing");

    public ConsumerTests()
    {
        if(Directory.Exists(TestingDirectory))
            Directory.Delete(TestingDirectory, true);

        Directory.CreateDirectory(TestingDirectory);
    }

    internal class MockPublication : IPublication
    {
        public async Task<bool> PublishStreamAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            return true;
        }
    }

    [Fact]
    public async Task Test1()
    {
        var distributed = new DistributedStorage(TestingDirectory);
        var staging = new TargetAreaStorage(distributed, StorageAreaType.Staging);

        var consumer = new PublishApkConsumer(new MockPublication(), distributed);

        var fp = Path.Join(Directory.GetCurrentDirectory(), "Test.apk");
        File.Create(fp).Dispose();
        var s = new SystemApkFile(fp);

        Assert.EndsWith("Staging\\V1_2_3.apk", (await staging.Store(s, "V1_2_3.apk", default)).FilePath);

        var apk = staging.EnumerateAll().First();
        Assert.True(await consumer.PublishApk(apk));


        Assert.EndsWith("Published\\V1_2_3.apk", apk.FilePath);

    }

    [Fact]
    public void StorageTest()
    {

    }
}


