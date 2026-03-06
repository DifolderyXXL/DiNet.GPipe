namespace DiNet.GPipe.Scheduler.Primitives;

public record struct PipelineResult(PipeActionResultState state, PipeActionResultState lastPipeState);

