using DiNet.GPipe.Domain;
using DiNet.GPipe.SharedKernel.Interfaces;

namespace DiNet.GPipe.Application.Versions;

public class VersionData
{
    public BuildVersion Latest { get; set; } = new BuildVersion(0, 0, 0);
}
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

public static class BoolExtensions 
{
    extension(bool state)
    {
        public int ToInt()
        {
            return state ? 1 : 0;
        }
    }    
}

