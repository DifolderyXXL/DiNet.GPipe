using DiNet.GPipe.Domain;

namespace DiNet.GPipe.SharedKernel.Interfaces;

public interface IVersionService
{
    public BuildVersion Put(BranchVersion version);
}


