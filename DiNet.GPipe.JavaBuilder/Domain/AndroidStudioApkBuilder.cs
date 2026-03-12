using DiNet.GPipe.JavaBuilder.Helpers;
using DiNet.GPipe.JavaBuilder.Settings;

namespace DiNet.GPipe.JavaBuilder.Domain;

public class AndroidStudioApkBuilder(
    AndroidStudioProjectSettings settings, 
    JdkSettings jdkSettings) : IApkBuilder
{
    private readonly AndroidStudioProjectSettings _settings = settings;
    private readonly JdkSettings _jdkSettings = jdkSettings;

    public async Task<string?> BuildAsync(CancellationToken token)
    {
        EnvironmentHelper.SetUpJdkEnvironment(_jdkSettings);

        await GradlewHelper.RunCleanDebugBuild(_settings, token);

        var path = _settings.GetBuildApkPath();

        return File.Exists(path) ? path : null;
    }
}

