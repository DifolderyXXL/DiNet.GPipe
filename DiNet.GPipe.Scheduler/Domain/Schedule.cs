using System;
using System.Collections.Generic;
using System.Text;

namespace DiNet.GPipe.Scheduler.Domain;

public class Schedule
{
    public Schedule(IPipeline taskPipeline, TimeSpan periodTime, SchedulePolicy policy = new()) 
    {
        TaskPipeline = taskPipeline;
        PeriodTime = periodTime;
        Policy = policy;
    }

    public IPipeline TaskPipeline { get; }
    public TimeSpan PeriodTime { get; }
    public SchedulePolicy Policy { get; }
}

