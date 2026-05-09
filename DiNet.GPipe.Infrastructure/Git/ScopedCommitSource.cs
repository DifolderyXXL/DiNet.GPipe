using DiNet.GPipe.Application;
using DiNet.GPipe.Application.Abstractions;
using DiNet.GPipe.Infrastructure.Project;
using LibGit2Sharp;
using Microsoft.Extensions.Logging;
using DiNet.GPipe.SharedKernel.Interfaces;
using DiNet.GPipe.Infrastructure.Database;

namespace DiNet.GPipe.Infrastructure.Git;

public class ScopedCommitSource(IProjectScopeContext context, ILogger<ScopedCommitSource> logger) : ICommitSource
{
    public string ProjectName => context.ProjectName;
    public int ProjectId => context.ProjectId;

    public IEnumerable<CommitInfo> GetCommitsSince(string branchName, string? commit)
    {
        using var repo = new Repository(context.GitUrl);

        var branch = repo.Branches[branchName];
        if(branch == null)
        {
            logger.LogWarning("Branch {Branch} do not exists in {Project}", branchName, context.ProjectName);
            return Enumerable.Empty<CommitInfo>();
        }

        var filter = new CommitFilter
        {
            IncludeReachableFrom = branch.Tip, 
            SortBy = CommitSortStrategies.Time | CommitSortStrategies.Reverse 
        };

        if (!string.IsNullOrEmpty(commit))
        {
            filter.ExcludeReachableFrom = commit;
        }

        return repo.Commits.QueryBy(filter)
            .Select(x => new CommitInfo(x.Sha, x.Message, x.Author.When.DateTime))
            .ToList();
    }

    public CommitInfo? GetTopCommitHash(string branchName)
    {
        using var repo = new Repository(context.GitUrl);

        var branch = repo.Branches[branchName];
        if (branch == null)
        {
            logger.LogWarning("Branch {Branch} do not exists in {Project}", branchName, context.ProjectName);
            return null;
        }

        var commit = branch.Tip;
        if (commit == null)
            return null;

        return new(commit.Sha, commit.Message, commit.Author.When.DateTime);
    }
}
