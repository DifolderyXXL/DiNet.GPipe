namespace DiNet.GPipe.JavaBuilder.Helpers;

public interface IProcessLogger
{
    void LogData(object sender, string? data);
    void LogError(object sender, string? error);
}

