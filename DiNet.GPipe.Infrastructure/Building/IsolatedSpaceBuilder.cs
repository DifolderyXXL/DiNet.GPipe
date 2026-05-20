using DiNet.GPipe.BuildingApplication.Apk;
using DiNet.GPipe.SharedKernel.Interfaces.Logging;
using DiNet.GPipe.SharedKernel.Models;
using DiNet.GPipe.SharedKernel.Results;

namespace DiNet.GPipe.Infrastructure.Building;

public class IsolatedSpaceBuilder(IApkBuilder api, IBuildWorkspace buildWorkspace) : IIsolatedBuilder
{
    public async Task<Result<IApkFile>> BuildIsolated(
        string repositoryUrl, string commitHash, BuildType buildType, IProcessLogger logger, CancellationToken cancellation)
    {
        var dir = await buildWorkspace.PrepareWorkspaceAsync(repositoryUrl, commitHash, cancellation);

        var result = await api.Build(dir, buildType, logger);

        return result;
    }

}