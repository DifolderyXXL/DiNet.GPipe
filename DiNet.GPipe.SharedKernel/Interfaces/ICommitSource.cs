namespace DiNet.GPipe.BackgroundWorker.Branches;

public interface ICommitSource
{
    public string ProjectName { get; }

    public CommitInfo? GetTopCommitHash(string branchName);
    public IEnumerable<CommitInfo> GetCommitsSince(string branchName, string? commit);
}
public record CommitInfo(string Hash, DateTime Date);