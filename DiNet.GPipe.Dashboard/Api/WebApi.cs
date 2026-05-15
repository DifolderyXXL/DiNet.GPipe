using DiNet.GPipe.Application.Handlers.Commits.Get;
using DiNet.GPipe.Application.Handlers.Projects.Get;
using DiNet.GPipe.Application.Handlers.Watchers;
using DiNet.GPipe.Application.Handlers.Watchers.Create;
using DiNet.GPipe.Domain;
using DiNet.GPipe.SharedKernel.Watchers;
using System.Net;

namespace DiNet.GPipe.Dashboard.Api;

public class MockWebApi : IWebApi
{
    public Task AddBranch(int projectId, BranchConfig branch, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<HttpStatusCode> CreateProject(CreateWatcherCommand command, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task DeleteProject(int projectId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<CommitResponse[]> GetProjectCommits(int projectId, bool includeActivity, CancellationToken ct)
    {
        CommitResponse[] commits = [new() { Name = "c1", BuildVersion = new(1, 2, 3), Hash = "asd123" }, new() { Name = "c3", BuildVersion = new(1, 2, 3), Hash = "asd123" }];
        return Task.FromResult(commits);
    }

    public Task<ProjectResponse[]> GetProjects(CancellationToken ct)
    {
        var projects = Enumerable.Range(0, 2).Select(x => new ProjectResponse(x, "Dad" + x, "url/url" + x, true, TimeSpan.Zero, [new("gena", Domain.BranchVersion.Beta)])).ToArray();

        return Task.FromResult(projects);
    }

    public Task<WatcherResponse?> GetProjectWatcher(int projectId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task RemoveBranch(int projectId, string branchName, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task UpdateBranch(int projectId, string branchName, BranchConfig branch, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task WatcherChangeActive(int projectId, bool active, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}

public class WebApi(HttpClient client) : IWebApi
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


    public async Task AddBranch(int projectId, BranchConfig branch, CancellationToken ct)
    {
        await client.PostAsJsonAsync($"/project/{projectId}/branch/add", branch, ct);
    }

    public async Task RemoveBranch(int projectId, string branchName, CancellationToken ct)
    {
        await client.PostAsync($"/project/{projectId}/branch/remove?branchName={branchName}", null, ct);
    }

    public async Task UpdateBranch(int projectId, string branchName, BranchConfig branch, CancellationToken ct)
    {
        await client.PostAsJsonAsync($"/project/{projectId}/branch/{branchName}/update", branch, ct);
    }

    public async Task WatcherChangeActive(int projectId, bool active, CancellationToken ct)
    {
        await client.PostAsync($"/watchers/{projectId}/chengeactive?activeState={active}", null, ct);
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