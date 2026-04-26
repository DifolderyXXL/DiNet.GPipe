using DiNet.GPipe.SharedKernel.Results;

namespace DiNet.GPipe.Application.Handlers.Messaging;

public interface ICommandHandler<in TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
    public Task<Result<TResponse>> Handle(TCommand command, CancellationToken ct); 
}

public interface ICommandHandler<TCommand>
    where TCommand : ICommand
{
    public Task<Result> Handle(TCommand command, CancellationToken ct);
}
