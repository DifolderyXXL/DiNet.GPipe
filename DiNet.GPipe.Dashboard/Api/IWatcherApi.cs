using System;
using DiNet.GPipe.Application.Handlers.Watchers;
using Refit;

namespace DiNet.GPipe.Dashboard.Api;


public interface IWatcherApi
{
    [Get("/watchers/{projectId}")]
    Task<WatcherResponse?> GetProjectWatcher(int projectId);

    [Post("/watchers/{projectId}/chengeactive")]
    Task<HttpResponseMessage> WatcherChangeActive(int projectId, bool active);
}
