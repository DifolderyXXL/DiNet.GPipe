using LibGit2Sharp;

namespace DiNet.GPipe.BackgroundWorker.Git;

public interface IGitRepositoryService
{
    public Task EnsureWorktreeCommit(string repositoryUrl, string directory, string commitHash, CancellationToken cancellation);
}

public class GitRepositoryService: IGitRepositoryService
{
    public async Task EnsureWorktreeCommit(string repositoryUrl, string directory, string commitHash, CancellationToken cancellation)
    {
        await Task.Run(() =>
        {
            var dir = Path.Combine(directory, "Target");
            if (Repository.IsValid(dir))
            {
                CheckoutCommit(dir, commitHash);
                return;
            }

            Directory.CreateDirectory(dir);
            
            Repository.Clone(repositoryUrl, dir);

            CheckoutCommit(dir, commitHash);
        });
    }

    private void CheckoutCommit(string dir, string commit)
    {
        using (var localRepo = new Repository(dir))
        {
            var localCommit = localRepo.Lookup<Commit>(commit);
            Commands.Checkout(localRepo, localCommit);
        }
    }
}