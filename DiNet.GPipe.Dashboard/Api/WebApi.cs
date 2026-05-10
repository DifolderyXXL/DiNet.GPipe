using DiNet.GPipe.Application.Handlers.Commits.Get;
using DiNet.GPipe.Application.Handlers.Projects.DeleteById;
using DiNet.GPipe.Application.Handlers.Projects.Get;
using DiNet.GPipe.Application.Handlers.Watchers;
using DiNet.GPipe.Application.Handlers.Watchers.Create;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiNet.GPipe.Dashboard.Api;



public class WebApi(HttpClient client)
{
    public async Task<ProjectResponse[]> GetProjects(CancellationToken ct)
        => await client.QueryArray<ProjectResponse>("/projects", ct);

    public async Task<WatcherResponse?> GetProjectWatcher(int projectId, CancellationToken ct)
        => await client.Query<WatcherResponse>($"/watchers/{projectId}", ct);

    public async Task<CommitResponse[]> GetProjectCommits(int projectId, bool includeActivity, CancellationToken ct)
        => await client.QueryArray<CommitResponse>(
            $"/project-commits?projectId={projectId}&includeActivity={includeActivity.ToString().ToLower()}", ct);


    public async Task<HttpStatusCode> CreateProject(CreateWatcherCommand command, CancellationToken ct)
    {
        return (await client.PostAsJsonAsync("/watcher", command, ct)).StatusCode;
    }

    public async Task DeleteProject(int projectId, CancellationToken ct)
    {
        await client.DeleteAsync($"/project?ProjectId={projectId}", ct);
    }

}

public static class ApiExtensions
{
    extension(HttpClient client)
    {
        public async Task<T?> Query<T>(string url, CancellationToken ct)
        {
            return await client.GetFromJsonAsync<T>(url, ct);
        }


        public async Task<T[]> QueryArray<T>(string url, CancellationToken ct)
        {
            return await client.QueryArray<T>(-1, url, ct);
        }
        public async Task<T[]> QueryArray<T>(int maximum, string url, CancellationToken ct)
        {
            List<T>? data = null;
            await foreach (var item in client.GetFromJsonAsAsyncEnumerable<T>(url, ct))
            {
                if (maximum >= 0 && data?.Count >= maximum)
                {
                    break;
                }
                if (item is not null)
                {
                    data ??= [];
                    data.Add(item);
                }
            }

            return data?.ToArray() ?? [];
        }
    }
    
}