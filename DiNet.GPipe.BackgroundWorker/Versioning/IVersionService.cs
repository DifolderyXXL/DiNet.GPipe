using DiNet.GPipe.BackgroundWorker.Common;

namespace DiNet.GPipe.BackgroundWorker.Versioning;

public interface IVersionService
{
    public Common.Version Put(BranchVersion version);
}


