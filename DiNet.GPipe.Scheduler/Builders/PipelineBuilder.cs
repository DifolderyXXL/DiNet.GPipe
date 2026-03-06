using DiNet.GPipe.Scheduler.Domain;
using DiNet.GPipe.Scheduler.Interfaces;

namespace DiNet.GPipe.Scheduler.Builders;

public class PipelineBuilder
{
    private List<IPipeAction> _actions = new();

    public PipelineBuilder Action(IPipeAction action)
    {
        _actions.Add(action);
        return this;
    }

    public DefaultPipeline Build()
    {
        return new(_actions.ToArray());
    }

    public static PipelineBuilder Create()
    {
        return new();
    }
}

