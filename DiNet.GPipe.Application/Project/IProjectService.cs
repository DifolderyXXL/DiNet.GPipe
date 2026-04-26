using DiNet.GPipe.Domain;
using DiNet.GPipe.SharedKernel.Watchers;

namespace DiNet.GPipe.Application.Project;

public interface IProjectService
{
    public Task<ProjectModel> GetOrCreateProject(WatcherRequest request);
}
