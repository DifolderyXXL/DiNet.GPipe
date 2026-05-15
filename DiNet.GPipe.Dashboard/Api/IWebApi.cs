using DiNet.GPipe.Application.Handlers.Commits.Get;
using DiNet.GPipe.Application.Handlers.Projects.Get;
using DiNet.GPipe.Application.Handlers.Watchers;
using DiNet.GPipe.Application.Handlers.Watchers.Create;
using DiNet.GPipe.SharedKernel.Watchers;
using System.Net;

namespace DiNet.GPipe.Dashboard.Api;

public interface IWebApi
{
    Task WatcherChangeActive(int projectId, bool active, CancellationToken ct);
    Task AddBranch(int projectId, BranchConfig branch, CancellationToken ct);
    Task<HttpStatusCode> CreateProject(CreateWatcherCommand command, CancellationToken ct);
    Task DeleteProject(int projectId, CancellationToken ct);
    Task<CommitResponse[]> GetProjectCommits(int projectId, bool includeActivity, CancellationToken ct);
    Task<ProjectResponse[]> GetProjects(CancellationToken ct);
    Task<WatcherResponse?> GetProjectWatcher(int projectId, CancellationToken ct);
    Task RemoveBranch(int projectId, string branchName, CancellationToken ct);
    Task UpdateBranch(int projectId, string branchName, BranchConfig branch, CancellationToken ct);
}