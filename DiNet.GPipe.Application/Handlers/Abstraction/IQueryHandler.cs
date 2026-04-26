using DiNet.GPipe.SharedKernel.Results;

namespace DiNet.GPipe.Application.Handlers.Messaging;

public interface IQueryHandler<in TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    public Task<Result<TResponse>> Handle(TQuery query, CancellationToken ct);
}
