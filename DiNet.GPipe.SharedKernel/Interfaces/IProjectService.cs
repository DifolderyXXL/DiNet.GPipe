using DiNet.GPipe.Domain;
using DiNet.GPipe.SharedKernel.Results;
using DiNet.GPipe.SharedKernel.Watchers;

namespace DiNet.GPipe.SharedKernel.Interfaces;

public interface IProjectService
{
    public Task<Result<ProjectModel>> CreateProject(WatcherRequest request, CancellationToken ct);
    public Task<Result> DeleteProject(int id, CancellationToken ct);
    public Task<Result> UpdateProjectName(int id, string newName, CancellationToken ct);
    public Task<Result> UpdateGitUrl(int id, string newGitUrl, CancellationToken ct);
}
