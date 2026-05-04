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
}

