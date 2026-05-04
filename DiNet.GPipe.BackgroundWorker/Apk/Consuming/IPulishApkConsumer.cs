using DiNet.GPipe.BuildingApplication.Apk;

namespace DiNet.GPipe.BuildingApplication.Apk.Consuming;

public interface IPulishApkConsumer
{
    public Task<bool> PublishApk(IApkFile file, CancellationToken cancellationToken = default);
}
