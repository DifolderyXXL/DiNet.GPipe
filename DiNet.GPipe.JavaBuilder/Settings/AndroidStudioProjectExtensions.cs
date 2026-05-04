namespace DiNet.GPipe.JavaBuilder.Settings;

public static class AndroidStudioProjectExtensions
{
    public static string GetBuildApkPath(string projectDirectory, ApkBuildType buildType)
    {
        var stringBuildType = buildType switch
        {
            ApkBuildType.Debug => "debug",
            ApkBuildType.ReleaseSigned => "release",
            _ => throw new NotImplementedException()
        };

        var stringBuildTypeSuffix = buildType switch
        {
            ApkBuildType.Debug => "debug",
            ApkBuildType.ReleaseSigned => "release",
            _ => throw new NotImplementedException()
        };


        return Path.Combine(projectDirectory, "app", "build",
                                "outputs", "apk",
                                stringBuildType, 
                                $"app-{stringBuildTypeSuffix}.apk");
    }
}

