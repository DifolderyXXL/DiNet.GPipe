using DiNet.GPipe.Domain;
using DiNet.GPipe.SharedKernel.Results;
using DiNet.GPipe.SharedKernel.Watchers;

namespace DiNet.GPipe.Application.Project;

public interface IProjectService
{
    public Task<Result<ProjectModel>> CreateProject(WatcherRequest request, CancellationToken ct);
    public Task<Result> DeleteProject(int id, CancellationToken ct);
}
