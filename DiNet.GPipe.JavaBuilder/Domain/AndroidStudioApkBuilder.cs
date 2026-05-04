using DiNet.GPipe.JavaBuilder.Helpers;
using DiNet.GPipe.JavaBuilder.Settings;

namespace DiNet.GPipe.JavaBuilder.Domain;

public class AndroidStudioApkBuilder(
    JdkSettings jdkSettings)
{
    private readonly JdkSettings _jdkSettings = jdkSettings;

    public async Task<string?> BuildAsync(string projectPath, ApkBuildType buildType, CancellationToken token)
    {
        EnvironmentHelper.SetUpJdkEnvironment(_jdkSettings);

        await GradlewHelper.RunCleanDebugBuild(projectPath, buildType, token);

        var path = AndroidStudioProjectExtensions.GetBuildApkPath(projectPath, buildType);

        return File.Exists(path) ? path : null;
    }
}

