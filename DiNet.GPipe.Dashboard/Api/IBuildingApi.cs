using DiNet.GPipe.Application.Handlers.Builds.GetActive;
using Refit;

namespace DiNet.GPipe.Dashboard.Api;

public interface IBuildingApi
{
    [Post("/builds/enqueue")]
    Task<HttpResponseMessage> EnqueueBuild(int projectId, string commitHash);

    [Get("/builds/active")]
    Task<ActiveBuildResponse[]> GetActiveBuilds();

    [Get("/builds/logs")]
    Task<string[]> GetBuildLogs(string builderId, int maxLines);
}