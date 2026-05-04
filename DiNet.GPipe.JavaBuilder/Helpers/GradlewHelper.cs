using DiNet.GPipe.JavaBuilder.Settings;

namespace DiNet.GPipe.JavaBuilder.Helpers;

public static class GradlewHelper
{
    public static async Task RunCleanDebugBuild(string path, ApkBuildType buildType, CancellationToken token = default)
    {
        await RunGradlewCommand(path, "clean");
        await RunGradlewCommand(path, $"assemble{buildType.ToString()} --stacktrace");
    }


    public static async Task RunGradlewCommand(string path, string command, CancellationToken token = default)
    {
        await ProcessHelper.RunProcess(Path.Combine(path, "gradlew.bat"), command, path);
    }
}

