using DiNet.GPipe.Application.Handlers.Commits.Get;
using DiNet.GPipe.Application.Handlers.Projects.Get;
using DiNet.GPipe.Application.Handlers.Watchers.Create;
using DiNet.GPipe.SharedKernel.Watchers;
using Refit;

namespace DiNet.GPipe.Dashboard.Api;

public interface IProjectApi
{
    [Get("/projects")]
    Task<ProjectResponse[]> GetProjects();

    [Get("/project-commits")]
    Task<CommitResponse[]> GetProjectCommits(int projectId, bool includeActivity);

    [Post("/watcher")]
    Task<HttpResponseMessage> CreateProject(CreateWatcherCommand command);

    [Delete("/project?ProjectId={projectId}")]
    Task<HttpResponseMessage> DeleteProject(int projectId);

    [Put("/project/{projectId}/name")]
    Task<HttpResponseMessage> UpdateProjectName(int projectId, string newName);

    [Put("/project/{projectId}/giturl")]
    Task<HttpResponseMessage> UpdateProjectGitUrl(int projectId, string newGitUrl);


    [Post("/project/{projectId}/branch/add")]
    Task<HttpResponseMessage> AddBranch(int projectId, BranchConfig branch);

    [Post("/project/{projectId}/branch/remove")]
    Task<HttpResponseMessage> RemoveBranch(int projectId, string branchName);

    [Post("/project/{projectId}/branch/{branchName}/update")]
    Task<HttpResponseMessage> UpdateBranch(int projectId, string branchName, BranchConfig branch);
}
