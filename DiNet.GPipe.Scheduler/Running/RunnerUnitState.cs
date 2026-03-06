using DiNet.GPipe.Scheduler.Primitives;

namespace DiNet.GPipe.Scheduler.Running;

public class RunnerUnitState
{
    public event Action<Exception>? OnException;
    public event Action<PipelineResult>? OnPipelineCompleted;

    private readonly ScheduleRunner _runner;
    public readonly int Id;
    public RunnerUnitState(ScheduleRunner runner, int id)
    {
        _runner = runner;
        Id = id;
    }

    public bool DestroyRunner()
    {
        return _runner.Destroy(Id);
    }

    internal void Send(PipelineResult result)
        => OnPipelineCompleted?.Invoke(result);

    internal void Send(Exception result)
        => OnException?.Invoke(result);
}


