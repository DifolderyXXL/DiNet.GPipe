using DiNet.GPipe.Application.Abstractions;
using LibGit2Sharp;

namespace DiNet.GPipe.Infrastructure.Git;

public class LibGit2RepositoryService: IGitRepositoryService
{
    public async Task EnsureWorktreeCommit(string repositoryUrl, string directory, string commitHash, CancellationToken cancellation)
    {
        await Task.Run(() =>
        {
            if (Repository.IsValid(directory))
            {
                CheckoutCommit(directory, commitHash);
                return;
            }

            Directory.CreateDirectory(directory);
            
            Repository.Clone(repositoryUrl, directory);

            CheckoutCommit(directory, commitHash);
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
