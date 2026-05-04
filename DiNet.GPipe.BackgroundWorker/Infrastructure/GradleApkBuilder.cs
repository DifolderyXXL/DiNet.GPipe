using DiNet.GPipe.BackgroundWorker.Apk;
using DiNet.GPipe.JavaBuilder;
using DiNet.GPipe.JavaBuilder.Domain;
using DiNet.GPipe.JavaBuilder.Settings;
using DiNet.GPipe.SharedKernel.Results;
using Microsoft.Extensions.Options;

namespace DiNet.GPipe.BuildingApplication.Infrastructure;

public class GradleApkBuilder(IOptions<JdkSettings> jdkSettings, IOptions<SignedReleaseBuildOptions> releaseBuildOptions) : IApkBuilder
{
    public async Task<Result<IApkFile>> Build(ApkBuildCommand command, CancellationToken cancellationToken = default)
    {
        var androidStudioBuilder = new AndroidStudioApkBuilder(jdkSettings.Value, releaseBuildOptions.Value);

        var apk = await androidStudioBuilder.BuildAsync(command.directory,
            command.type switch
            {
                SharedKernel.Models.BuildType.Debug => ApkBuildType.Debug,
                SharedKernel.Models.BuildType.Release => ApkBuildType.ReleaseSigned,
                _ => throw new NotImplementedException()
            },
            cancellationToken);

        return
            apk == null ? new Error("Gradlew failed", ErrorType.Gradlew) :
            new SystemApkFile(apk);
    }
}