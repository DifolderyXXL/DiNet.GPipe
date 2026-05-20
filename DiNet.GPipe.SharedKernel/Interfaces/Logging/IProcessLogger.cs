namespace DiNet.GPipe.Infrastructure.Building.V2;

/// <summary>
/// Scoped logger for logging from specific builder instance / specific project
/// </summary>
public interface IProcessLogger
{
    void Log(string message);
    event Action<string> OnLog;
    IReadOnlyList<string> GetRecentLogs(int maxLines);
    IAsyncEnumerable<string> ReadAllLogsAsync(CancellationToken ct);
}
