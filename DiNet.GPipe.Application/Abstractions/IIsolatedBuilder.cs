using DiNet.GPipe.BuildingApplication.Apk;
using DiNet.GPipe.SharedKernel.Models;
using DiNet.GPipe.SharedKernel.Results;

namespace DiNet.GPipe.Infrastructure.Building;

public interface IIsolatedBuilder
{
    Task<Result<IApkFile>> BuildIsolated(string repositoryUrl, string commitHash, BuildType buildType, CancellationToken cancellation);
}