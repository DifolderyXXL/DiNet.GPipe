using DiNet.GPipe.SharedKernel.Models;
using DiNet.GPipe.SharedKernel.Results;

namespace DiNet.GPipe.BuildingApplication.Apk;

public interface IApkBuilder
{
    public Task<Result<IApkFile>> Build(string directory, BuildType type, CancellationToken cancellationToken = default);
}
