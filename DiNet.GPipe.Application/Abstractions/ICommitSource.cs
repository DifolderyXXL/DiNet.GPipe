namespace DiNet.GPipe.Application.Abstractions;

public interface ICommitSource
{
    public string ProjectName { get; }
    public int ProjectId { get; }

    public CommitInfo? GetTopCommitHash(string branchName);
    public IEnumerable<CommitInfo> GetCommitsSince(string branchName, string? commit);
}
public record CommitInfo(string Hash, string Name, DateTime Date);