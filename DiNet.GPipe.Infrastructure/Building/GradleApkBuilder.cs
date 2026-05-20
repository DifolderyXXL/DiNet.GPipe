using DiNet.GPipe.BuildingApplication.Apk;
using DiNet.GPipe.JavaBuilder;
using DiNet.GPipe.JavaBuilder.Domain;
using DiNet.GPipe.JavaBuilder.Helpers;
using DiNet.GPipe.JavaBuilder.Settings;
using DiNet.GPipe.SharedKernel.Interfaces.Logging;
using DiNet.GPipe.SharedKernel.Models;
using DiNet.GPipe.SharedKernel.Results;
using Microsoft.Extensions.Options;

namespace DiNet.GPipe.BuildingApplication.Infrastructure;

public class JavaProcessLoggerAdapter(IProcessLogger processLogger) : IJavaProcessLogger
{
    public void LogData(object sender, string? data)
    {
        processLogger.Log($"[{sender}][{data}]");
    }

    public void LogError(object sender, string? error)
    {
        processLogger.Log($"[{sender}][{error}]");
    }
}
public class GradleApkBuilder(
    IOptions<JdkSettings> jdkSettings,
    IOptions<SignedReleaseBuildOptions> releaseBuildOptions) : IApkBuilder
{
    public async Task<Result<IApkFile>> Build(string directory, BuildType type, IProcessLogger logger, CancellationToken cancellationToken = default)
    {
        var androidStudioBuilder = new AndroidStudioApkBuilder(
            jdkSettings.Value, releaseBuildOptions.Value, new JavaProcessLoggerAdapter(logger));

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