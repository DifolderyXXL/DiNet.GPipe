using DiNet.GPipe.SharedKernel.Interfaces;
using Moq;
using DiNet.GPipe.BuildingApplication.Apk;
using DiNet.GPipe.BuildingApplication.Apk.Consuming;
using DiNet.GPipe.Application.Versions;

namespace DiNet.GPipe.Tests;

public class ConsumerTests
{
    private string TestingDirectory = Path.Join(Directory.GetCurrentDirectory(), "Testing");

    public ConsumerTests()
    {
        if (Directory.Exists(TestingDirectory))
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
       /* var distributed = new DistributedStorage(TestingDirectory);
        var staging = new TargetAreaStorage(distributed, StorageAreaType.Staging);

        var consumer = new PublishApkConsumer(new MockPublication(), distributed);

        var fp = Path.Join(Directory.GetCurrentDirectory(), "Test.apk");
        File.Create(fp).Dispose();
        var s = new SystemApkFile(fp);

        Assert.EndsWith("Staging\\V1_2_3.apk", (await staging.Store(s, "V1_2_3.apk", default)).FilePath);

        var apk = staging.EnumerateAll().First();
        Assert.True(await consumer.PublishApk(apk));


        Assert.EndsWith("Published\\V1_2_3.apk", apk.FilePath);*/

    }

    [Fact]
    public async Task TestBuild()
    {
        var mockRepository = new Mock<IBuildRegistryRepository>();
        //var mockStagingStorage = new Mock<IApkStagingStorage>();
        var mockApkProvider = new Mock<IApkBuilder>();
        var versionService = new Mock<IVersionService>();

       /* mockStagingStorage.Setup(_ => _.Store(It.IsAny<IApkFile>(), It.IsAny<BuildType>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new SystemApkFile("test"));*/

        mockApkProvider.Setup(_ => _.Build(It.IsAny<ApkBuildCommand>()))
            .ReturnsAsync(new SystemApkFile("testapk"));

        //versionService.Setup(_ => _.Put(It.IsAny<BranchVersion>()))
        //    .Returns(new BuildVersion(1,1,1));

        /*
        var buildService = new BuildService(mockRepository.Object, mockStagingStorage.Object, mockApkProvider.Object, versionService.Object);

        await buildService.Build("N", "hs", BranchVersion.Release, default);

        mockRepository.Verify(_ => _.Save(It.Is<BuildRecord>(m => m.BuildPath == "test")), Times.Once());
        */
    }
}