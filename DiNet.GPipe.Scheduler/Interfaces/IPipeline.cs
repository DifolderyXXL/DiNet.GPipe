using DiNet.GPipe.Scheduler.Primitives;

namespace DiNet.GPipe.Scheduler;

public interface IPipeline
{
    public PipelineResult RunPipeline();
}

