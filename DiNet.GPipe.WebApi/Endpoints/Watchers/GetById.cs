using DiNet.GPipe.Application.Handlers.Messaging;
using DiNet.GPipe.Application.Handlers.Watchers;
using DiNet.GPipe.Application.Handlers.Watchers.GetById;
using DiNet.GPipe.SharedKernel.Results;
using DiNet.GPipe.WebApi.Infrastructure;

namespace DiNet.GPipe.WebApi.Endpoints.Watchers;

public class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/watchers/{id}", async (
            int id,
            IQueryHandler<GetWatcherByIdQuery, WatcherResponse> handler,
            CancellationToken ct) =>
        {
            var query = new GetWatcherByIdQuery(id);

            var result = await handler.Handle(query, ct);

            return result.Match(Results.Ok, CustomResults.Problem);

        });
    }
}
