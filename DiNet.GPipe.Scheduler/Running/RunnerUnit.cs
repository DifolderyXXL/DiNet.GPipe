using DiNet.GPipe.Scheduler.Domain;
using DiNet.GPipe.Scheduler.Primitives;

namespace DiNet.GPipe.Scheduler.Running;

public class RunnerUnit : IDisposable
{
    private readonly RunnerUnitState _state;
    private Task? _runningTask;
    private PeriodicTimer? _timer;
    private CancellationTokenSource? _cts;

    
    public RunnerUnit(RunnerUnitState state, Schedule schedule)
    {
        _state = state;
        Schedule = schedule;
    }

    public Schedule Schedule { get; }
    public bool IsRunning => _runningTask != null 
        && (!_runningTask.IsCompleted && !_runningTask.IsCanceled && !_runningTask.IsFaulted);

    public void Dispose()
    {
        Stop();
    }

    public void Run()
    {
        if (IsRunning) return;

        _timer = new PeriodicTimer(Schedule.PeriodTime);
        _cts = new();

        _runningTask = InternalRun(_cts.Token);
    }

    public void Stop()
    {
        _cts?.Cancel();
        _cts?.Dispose();

        _runningTask?.Wait();
        _runningTask?.Dispose();

        _timer?.Dispose();

        _cts = null;
        _timer = null;
        _runningTask = null;
    }

    private async Task InternalRun(CancellationToken token)
    {
        while(!_cts!.IsCancellationRequested && await _timer!.WaitForNextTickAsync(token))
        {
            try
            {
                var result = Schedule.TaskPipeline.RunPipeline();
                _state.Send(result);
            }
            catch(Exception ex)
            {
                _state.Send(ex);

                switch (Schedule.Policy.ExceptionPolicy)
                {
                    case ScheduleExceptionPolicy.Ignore:
                        break;
                    case ScheduleExceptionPolicy.Destroy:
                        _state.DestroyRunner();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}


