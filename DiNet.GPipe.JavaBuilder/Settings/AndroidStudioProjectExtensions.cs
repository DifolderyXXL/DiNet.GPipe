namespace DiNet.GPipe.JavaBuilder.Settings;

public static class AndroidStudioProjectExtensions
{
    public static string GetBuildApkPath(string projectDirectory, ApkBuildType buildType)
    {
        var stringBuildType = buildType switch
        {
            ApkBuildType.Debug => "debug",
            ApkBuildType.Release => "release",
            _ => throw new NotImplementedException()
        };

        return Path.Combine(projectDirectory, "app", "build",
                                "outputs", "apk",
                                stringBuildType, 
                                $"app-{stringBuildType}.apk");
    }
}

