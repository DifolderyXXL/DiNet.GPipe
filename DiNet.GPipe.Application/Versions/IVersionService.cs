using DiNet.GPipe.Domain;

namespace DiNet.GPipe.Application.Versions;

public interface IVersionService
{
    public BuildVersion Put(BranchVersion version);
}


