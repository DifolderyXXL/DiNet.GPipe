
using DiNet.GPipe.SharedKernel;

namespace DiNet.GPipe.BackgroundWorker.Branches;

public interface ICommitSource
{
    public string ProjectName { get; }
    public string BranchName { get; }

    public BranchVersion TargetVersionType { get; }

    public string? GetTopCommitHash();
    public string? GetNextCommitHash(string current);
}
