using DiNet.GPipe.BackgroundWorker.Common;
using DiNet.GPipe.SharedKernel.Models;

namespace DiNet.GPipe.BackgroundWorker.Versioning;

public interface IVersionService
{
    public BuildVersion Put(BranchVersion version);
}


