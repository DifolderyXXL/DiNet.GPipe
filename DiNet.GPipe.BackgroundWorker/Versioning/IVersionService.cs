using DiNet.GPipe.BackgroundWorker.Common;

namespace DiNet.GPipe.BackgroundWorker.Versioning;

public interface IVersionService
{
    public Common.BuildVersion Put(BranchVersion version);
}


