namespace DiNet.GPipe.JavaBuilder.Settings;

public record AndroidStudioProjectSettings(
    string path,
    ApkBuildType buildType)
{
    public string GetBuildApkPath()
    {
        var stringBuildType = buildType switch
        {
            ApkBuildType.Debug => "debug",
            ApkBuildType.Release => "release",
            _ => throw new NotImplementedException()
        };

        return Path.Combine(path, "app", "build",
                                "outputs", "apk",
                                stringBuildType, 
                                $"app-{stringBuildType}.apk");
    }
}

