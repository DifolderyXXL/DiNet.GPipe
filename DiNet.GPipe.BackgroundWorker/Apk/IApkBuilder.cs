using DiNet.GPipe.SharedKernel.Results;
using DiNet.GPipe.BuildingApplication.Apk;

namespace DiNet.GPipe.BuildingApplication.Apk;

public interface IApkBuilder
{
    public Task<Result<IApkFile>> Build(ApkBuildCommand command, CancellationToken cancellationToken = default);
}
