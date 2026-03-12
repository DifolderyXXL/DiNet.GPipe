using DiNet.GPipe.JavaBuilder.Settings;

namespace DiNet.GPipe.JavaBuilder.Helpers;

public static class GradlewHelper
{
    public static async Task RunCleanDebugBuild(AndroidStudioProjectSettings settings, CancellationToken token = default)
    {
        await RunGradlewCommand(settings, "clean");
        await RunGradlewCommand(settings, "assembleDebug --stacktrace");
    }


    public static async Task RunGradlewCommand(AndroidStudioProjectSettings settings, string command, CancellationToken token = default)
    {
        await ProcessHelper.RunProcess(Path.Combine(settings.path, "gradlew.bat"), command, settings.path);
    }
}

