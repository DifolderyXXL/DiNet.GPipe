using DiNet.GPipe.Scheduler.Interfaces;
using DiNet.GPipe.Scheduler.Primitives;

namespace DiNet.GPipe.Scheduler.Domain;

public class DefaultPipeline : IPipeline
{
    private readonly IPipeAction[] _actions;

    public DefaultPipeline(IPipeAction[] actions)
    {
        _actions = actions;
    }

    public PipelineResult RunPipeline()
    {
        var lastPipeResult = PipeActionResultState.None;
        foreach (var action in _actions)
        {
            var result = action.Run();

            if (result.state != PipeActionResultState.Success)
            {
                lastPipeResult = result.state;
                return new(PipeActionResultState.Failure, lastPipeResult);
            }
        }

        return new(PipeActionResultState.Success, lastPipeResult);
    }
}

