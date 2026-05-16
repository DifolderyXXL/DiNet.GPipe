using DiNet.GPipe.JavaBuilder.Settings;

namespace DiNet.GPipe.JavaBuilder.Helpers;

public static class GradlewHelper
{
    public static async Task RunCleanBuild(string path, ApkBuildType buildType, IProcessLogger? logger = null, CancellationToken token = default)
    {
        await RunGradlewCommand(path, "clean", logger);
        await RunGradlewCommand(path, $"assemble{buildType.ToString()} --stacktrace", logger);
    }


    public static async Task RunReleaseSignedBuild(
        string path,
        string keystorePath,
        string storePassword,
        string keyAlias,
        string keyPassword,
        IProcessLogger? logger = null)
    {
        var command = 
                     $"assembleRelease --stacktrace " +
                     $"-Pandroid.injected.signing.store.file=\"{keystorePath}\" " +
                     $"-Pandroid.injected.signing.store.password={storePassword} " +
                     $"-Pandroid.injected.signing.key.alias={keyAlias} " +
                     $"-Pandroid.injected.signing.key.password={keyPassword}";

        await RunGradlewCommand(path, "clean", logger);
        await RunGradlewCommand(path, command, logger);
    }


    public static async Task RunGradlewCommand(string path, string command, IProcessLogger? logger = null, CancellationToken token = default)
    {
        await ProcessHelper.RunProcess(Path.Combine(path, "gradlew.bat"), command, path, logger);
    }
}

