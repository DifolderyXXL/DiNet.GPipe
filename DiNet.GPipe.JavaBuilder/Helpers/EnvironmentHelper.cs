using DiNet.GPipe.JavaBuilder.Settings;

namespace DiNet.GPipe.JavaBuilder.Helpers;

public static class EnvironmentHelper
{
    public static bool SetUpJdkEnvironment(JdkSettings settings)
    {
        if (!Directory.Exists(settings.AndroidStudioJdk))
        {
            return false;
        }
        
        // PowerShell: $env:JAVA_HOME = $androidStudioJdk
        Environment.SetEnvironmentVariable("JAVA_HOME", settings.AndroidStudioJdk,
                                            EnvironmentVariableTarget.Process);

        // PowerShell: $env:PATH = "$androidStudioJdk\bin;$env:PATH"
        string? currentPath = Environment.GetEnvironmentVariable("PATH",
                                        EnvironmentVariableTarget.Process);

        if (currentPath == null)
        {
            return false;
        }

        string newPath = $@"{settings.AndroidStudioJdk}\bin;" + currentPath;
        Environment.SetEnvironmentVariable("PATH", newPath,
                                            EnvironmentVariableTarget.Process);

        return true;
    }

    public static bool SetUpSDKEnvironment(JdkSettings settings)
    {
        if (!Directory.Exists(settings.AndroidStudioSDK))
            return false;

        Environment.SetEnvironmentVariable("ANDROID_HOME", settings.AndroidStudioSDK, EnvironmentVariableTarget.Process);
        Environment.SetEnvironmentVariable("ANDROID_SDK_ROOT", settings.AndroidStudioSDK, EnvironmentVariableTarget.Process);

        string? currentPath = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Process);
        if (currentPath != null)
        {
            string newPath = $@"{settings.AndroidStudioSDK}\platform-tools;{settings.AndroidStudioSDK}\tools;{currentPath}";
            Environment.SetEnvironmentVariable("PATH", newPath, EnvironmentVariableTarget.Process);
        }
        return true;
    }
}

