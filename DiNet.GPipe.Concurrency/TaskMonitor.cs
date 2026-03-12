using System.Collections.Concurrent;
using System.Collections.Immutable;

public class TaskMonitor
{
    private ConcurrentDictionary<int, Task> _tasks = new();

    public void WaitAllTasksCompleted(CancellationToken token = default)
    {
        Task.WaitAll(_tasks.Values, token);
    }

    public bool Enqueue(Func<CancellationToken, Task> task, CancellationToken token)
    {
        var internalTask = InternalWorker(task, token);
        return true;
    }

    public int GetRegisteredTaskCount()
    {
        return _tasks.Count;
    }

    private async Task InternalWorker(Func<CancellationToken, Task> task, CancellationToken token)
    {
        var targetTask = Task.Run(async () =>
        {
            await task.Invoke(token);
        }, token);

        try
        {
            _tasks.TryAdd(targetTask.Id, targetTask);

            await targetTask.WaitAsync(token);
        }
        finally
        {
            _tasks.TryRemove(targetTask.Id, out var _);
        }
    }

}