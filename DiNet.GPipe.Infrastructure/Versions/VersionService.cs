using DiNet.GPipe.Application.Versions;
using DiNet.GPipe.Domain;
using DiNet.GPipe.SharedKernel.Interfaces;

namespace DiNet.GPipe.Infrastructure.Versions;
public class VersionService(IDataRepository<VersionData> repository) : IVersionService
{
    public BuildVersion Put(BranchVersion version)
    {
        var vers = repository.Get() ?? new();

        vers.Latest = version switch
        {
            BranchVersion.Release => vers.Latest with { release = vers.Latest.release + 1, beta = 0, alpha = 0 },
            BranchVersion.Beta => vers.Latest with { release = vers.Latest.release, beta = vers.Latest.beta + 1, alpha = 0 },
            BranchVersion.Alpha => vers.Latest with { release = vers.Latest.release, beta = vers.Latest.beta, alpha = vers.Latest.alpha + 1 },
            _ => throw new NotImplementedException(),
        };

        repository.Save(vers);

        return vers.Latest;
    }
}

