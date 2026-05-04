using DiNet.GPipe.SharedKernel.Results;
using DiNet.GPipe.Application.Handlers.Abstraction;

namespace DiNet.GPipe.Application.Handlers.Abstraction;

public interface IQueryHandler<in TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    public Task<Result<TResponse>> Handle(TQuery query, CancellationToken ct);
}
