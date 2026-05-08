namespace DiNet.GPipe.Application.Abstractions;

public interface IGitRepositoryService
{
    public Task EnsureWorktreeCommit(string repositoryUrl, string directory, string commitHash, CancellationToken cancellation);
}
