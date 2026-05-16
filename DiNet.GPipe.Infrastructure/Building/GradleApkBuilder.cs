using DiNet.GPipe.BuildingApplication.Apk;
using DiNet.GPipe.JavaBuilder;
using DiNet.GPipe.JavaBuilder.Domain;
using DiNet.GPipe.JavaBuilder.Helpers;
using DiNet.GPipe.JavaBuilder.Settings;
using DiNet.GPipe.SharedKernel.Models;
using DiNet.GPipe.SharedKernel.Results;
using Microsoft.Extensions.Options;

namespace DiNet.GPipe.BuildingApplication.Infrastructure;

public class GradleApkBuilder(
    IOptions<JdkSettings> jdkSettings, 
    IOptions<SignedReleaseBuildOptions> releaseBuildOptions,
    IProcessLogger? processLogger = null) : IApkBuilder
{
    public async Task<Result<IApkFile>> Build(string directory, BuildType type, CancellationToken cancellationToken = default)
    {
        var androidStudioBuilder = new AndroidStudioApkBuilder(jdkSettings.Value, releaseBuildOptions.Value, processLogger);

        var apk = await androidStudioBuilder.BuildAsync(directory,
            type switch
            {
                BuildType.Debug => ApkBuildType.Debug,
                BuildType.Release => ApkBuildType.ReleaseSigned,
                _ => throw new NotImplementedException()
            },
            cancellationToken);

        return
            apk == null ? new Error("Gradlew failed", ErrorType.Gradlew) :
            new SystemApkFile(apk);
    }
}