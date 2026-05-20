using DiNet.GPipe.SharedKernel.Interfaces.Logging;
using Microsoft.AspNetCore.SignalR;

namespace DiNet.GPipe.WebApi.Hubs;

public class SignalRForwardingLogger(
    IProcessLogger processLogger,
    IHubContext<BuildLogHub> hubContext,
    string buildId) : IProcessLogger
{
    public event Action<string>? OnLog
    {
        add => processLogger.OnLog += value;
        remove => processLogger.OnLog -= value;
    }

    public IReadOnlyList<string> GetRecentLogs(int maxLines)
        => processLogger.GetRecentLogs(maxLines);


    public void Log(string message)
    {
        processLogger.Log(message);

        _ = hubContext.Clients.Group($"build-{buildId}")
            .SendAsync("ReceiveLog", message);
    }

    public IAsyncEnumerable<string> ReadAllLogsAsync(CancellationToken ct)
        => processLogger.ReadAllLogsAsync(ct);
}