using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace DiNet.GPipe.Infrastructure.Building.V2;

public class ProcessLogger : IProcessLogger
{
    private readonly ConcurrentQueue<string> _logs = new();
    private const int MaxLogHistory = 1000;

    public event Action<string>? OnLog;

    public void Log(string message)
    {
        var formatted = $"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] {message}";
        _logs.Enqueue(formatted);

        while (_logs.Count > MaxLogHistory)
        {
            _logs.TryDequeue(out _);
        }

        OnLog?.Invoke(formatted);
    }

    public IReadOnlyList<string> GetRecentLogs(int maxLines)
    {
        return _logs.TakeLast(maxLines).ToList();
    }

    public async IAsyncEnumerable<string> ReadAllLogsAsync([EnumeratorCancellation] CancellationToken ct)
    {
        foreach (var log in _logs)
        {
            if (ct.IsCancellationRequested) yield break;
            yield return log;
        }
        await Task.CompletedTask;
    }
}