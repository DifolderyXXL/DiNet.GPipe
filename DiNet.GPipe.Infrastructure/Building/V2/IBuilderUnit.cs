using DiNet.GPipe.BuildingApplication.Apk;
using DiNet.GPipe.SharedKernel.Results;

namespace DiNet.GPipe.Infrastructure.Building.V2;

public interface IBuilderUnit
{
    Task<Result<IApkFile>> RunAsync(BuildRequest request, IProcessLogger logger, CancellationToken ct);
}
