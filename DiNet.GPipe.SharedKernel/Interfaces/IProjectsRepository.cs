using DiNet.GPipe.Domain;

namespace DiNet.GPipe.SharedKernel.Interfaces;

public interface IProjectsRepository
{
    public Task<ProjectModel?> Get(int id);
    public Task Add(ProjectModel project);
    public Task<bool> Contains(int id);
    public Task<ProjectModel?> GetByGitUrl(string gitUrl);

    public Task<bool> Delete(int id);

    public IEnumerable<ProjectModel> EnumerateAllReadonly();

    public Task SaveAsync();
}