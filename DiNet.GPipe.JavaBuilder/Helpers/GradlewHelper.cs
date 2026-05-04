using DiNet.GPipe.JavaBuilder.Settings;

namespace DiNet.GPipe.JavaBuilder.Helpers;

public static class GradlewHelper
{
    public static async Task RunCleanBuild(string path, ApkBuildType buildType, CancellationToken token = default)
    {
        await RunGradlewCommand(path, "clean");
        await RunGradlewCommand(path, $"assemble{buildType.ToString()} --stacktrace");
    }


    public static async Task RunReleaseSignedBuild(string path, string keystorePath, string storePassword, string keyAlias, string keyPassword)
    {
        var command = 
                     $"assembleRelease --stacktrace " +
                     $"-Pandroid.injected.signing.store.file=\"{keystorePath}\" " +
                     $"-Pandroid.injected.signing.store.password={storePassword} " +
                     $"-Pandroid.injected.signing.key.alias={keyAlias} " +
                     $"-Pandroid.injected.signing.key.password={keyPassword}";

        await RunGradlewCommand(path, "clean");
        await RunGradlewCommand(path, command);
    }


    public static async Task RunGradlewCommand(string path, string command, CancellationToken token = default)
    {
        await ProcessHelper.RunProcess(Path.Combine(path, "gradlew.bat"), command, path);
    }
}

