using DiNet.GPipe.Scheduler.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace DiNet.GPipe.Scheduler.Running;

public class ScheduleRunner
{

    private Dictionary<int, RunnerUnit> _timers = new();
    private int _count = 0;

    private readonly Lock _locker = new Lock();

    public ScheduleRunner()
    {
        
    }

    public RunnerUnitState Add(Schedule schedule)
    {
        lock (_locker)
        {
            var id = _count++;
            var state = new RunnerUnitState(this, id);
            var runner = new RunnerUnit(state, schedule);

            ProcessCreatedRunner(runner);

            _timers.Add(id, runner);
            return state;
        }
    }

    public bool Destroy(int id)
    {
        lock (_locker)
        {
            if(_timers.TryGetValue(id, out var runner))
            {
                runner.Dispose();
                _timers.Remove(id);
                return true;
            }
            return false;
        }
    }

    public bool IsRunning(int id)
    {
        if(_timers.TryGetValue(id, out var runner))
        {
            return runner.IsRunning;
        }

        return false;
    }

    private void ProcessCreatedRunner(RunnerUnit runner)
    {
        runner.Run();
    }
}