public class SingleExecutionWorker
{
    private Task? _activeTask;
    private readonly object _lock = new object();

    public bool TryRun(Func<CancellationToken, Task> task, CancellationToken token)
    {
        lock (_lock)
        {
            if (_activeTask != null && !_activeTask.IsCompleted)
                return false;
                
            _activeTask = InternalWorker(task, token);
        }
        return true;
    }

    private async Task InternalWorker(Func<CancellationToken, Task> task, CancellationToken token)
    {
        try
        {
            await task.Invoke(token);
        }
        finally
        {
            lock (_lock)
            {
                _activeTask = null;
            }
        }
    }
}
