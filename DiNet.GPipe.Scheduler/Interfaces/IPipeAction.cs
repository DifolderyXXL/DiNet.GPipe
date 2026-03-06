using DiNet.GPipe.Scheduler.Primitives;

namespace DiNet.GPipe.Scheduler.Interfaces;

public interface IPipeAction
{
    public PipeActionResult Run();
}

