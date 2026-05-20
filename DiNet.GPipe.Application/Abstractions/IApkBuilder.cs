using DiNet.GPipe.SharedKernel.Interfaces.Logging;
using DiNet.GPipe.SharedKernel.Models;
using DiNet.GPipe.SharedKernel.Results;

namespace DiNet.GPipe.BuildingApplication.Apk;

public interface IApkBuilder
{
    public Task<Result<IApkFile>> Build(string directory, BuildType type, IProcessLogger logger, CancellationToken cancellationToken = default);
}
