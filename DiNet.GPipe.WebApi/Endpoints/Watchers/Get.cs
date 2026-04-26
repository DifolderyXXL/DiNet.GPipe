using DiNet.GPipe.Application.Handlers.Messaging;
using DiNet.GPipe.Application.Handlers.Watchers;
using DiNet.GPipe.Application.Handlers.Watchers.GetByUrl;
using DiNet.GPipe.SharedKernel.Results;
using DiNet.GPipe.WebApi.Infrastructure;

namespace DiNet.GPipe.WebApi.Endpoints.Watchers;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/watchers", async (
            IQueryHandler<GetWatchersQuery, List<WatcherResponse>> handler,
            CancellationToken ct) =>
        {
            var query = new GetWatchersQuery();

            var result = await handler.Handle(query, ct);

            return result.Match(Results.Ok, CustomResults.Problem);

        });
    }
}
