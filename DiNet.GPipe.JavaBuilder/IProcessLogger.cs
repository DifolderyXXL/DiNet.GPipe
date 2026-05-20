namespace DiNet.GPipe.JavaBuilder.Helpers;

public interface IJavaProcessLogger
{
    void LogData(object sender, string? data);
    void LogError(object sender, string? error);
}

