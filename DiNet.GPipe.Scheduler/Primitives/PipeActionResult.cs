namespace DiNet.GPipe.Scheduler.Primitives;

public record struct PipeActionResult(PipeActionResultState state)
{
    public static readonly PipeActionResult Success = new PipeActionResult(PipeActionResultState.Success);
    public static readonly PipeActionResult Failure = new PipeActionResult(PipeActionResultState.Failure);
}

