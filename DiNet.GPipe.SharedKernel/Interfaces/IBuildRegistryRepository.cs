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
