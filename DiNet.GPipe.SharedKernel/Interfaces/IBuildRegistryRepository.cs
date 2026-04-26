using DiNet.GPipe.Domain;

namespace DiNet.GPipe.SharedKernel.Interfaces;


public interface IBuildRegistryRepository
{
    public Task Add(BuildRegistry build);
    public Task<BuildRegistry?> Get(string hash);
    public Task<bool> Contains(string hash);

    public Task SaveAsync();

    public Task UpdateStatus(string commitHash, BuildStatus status);

    public Task CreateFailedRecord(BuildVersion? version, string commitHash, string error);
}

public interface IProjectsRepository
{
    public Task<ProjectModel?> Get(int id);
    public Task Add(ProjectModel project);
    public Task<bool> Contains(int id);
    public Task<ProjectModel?> GetByGitUrl(string gitUrl);

    public Task SaveAsync();
}