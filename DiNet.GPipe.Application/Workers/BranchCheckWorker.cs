using DiNet.GPipe.BackgroundWorker.Branches;
using DiNet.GPipe.Domain;
using DiNet.GPipe.SharedKernel.Interfaces;
using DiNet.GPipe.SharedKernel.Interfaces.Messaging;
using DiNet.GPipe.SharedKernel.Models.Commands;
using DiNet.GPipe.SharedKernel.Watchers;
using Microsoft.Extensions.Logging;

namespace DiNet.GPipe.Application.Workers;

public record WatcherParameters(
    ProjectModel Project,
    List<BranchConfig> Branches,
    TimeSpan Period
    );

public class BranchData
{
    public Dictionary<string, string> LastSeenHashes { get; set; } = new();
}


public class BranchCheckWorker (WatcherParameters parameters,
                               IVersionService versionService,
                               ICommitSource commitSource,
                               IDataRepository<BranchData> branchRepository,
                               IBuildRegistryRepository buildRegistry,
                               IEventBus eventBus,
                               ILogger<BranchCheckWorker> logger) : IWatcherWorker
{
    public void Start(CancellationToken ct)
    {
        _ = ExecuteAsync(ct);
    }

    public async Task ExecuteAsync(CancellationToken ct)
    {
        var timer = new PeriodicTimer(parameters.Period);

        while (await timer.WaitForNextTickAsync(ct))
        {
            try
            {
                await CheckForNewCommits(ct);
            }
            catch(Exception e)
            {
                logger.LogError(e, "Error while checking commits for {Project}", parameters.Project.Name);
            }
        }
    }

    private async Task CheckForNewCommits(CancellationToken ct)
    {
        var state = branchRepository.Get() ?? new();

        var allNewCommits = new List<(CommitInfo commit, BranchConfig branch)>();
        foreach (var branch in parameters.Branches)
        {
            state.LastSeenHashes.TryGetValue(branch.BranchName, out var lastHash);

            var delta = commitSource.GetCommitsSince(branch.BranchName, lastHash);
            allNewCommits.AddRange(delta.Select(x=>(x, branch)));
        }

        var orderedCommits = allNewCommits
            .GroupBy(c=>c.commit.Hash)
            .Select(g => g.First())
            .OrderByDescending(c => c.branch.VersionType)
            .ThenBy(c => c.commit.Date);

        foreach(var commit in orderedCommits)
        {
            if (await buildRegistry.Contains(commit.commit.Hash)) continue;

            var version = versionService.Put(commit.branch.VersionType);

            var command = new CommitDetected(
               commitSource.ProjectName,
               commit.branch.BranchName,
               commit.commit.Hash,
               version,
               Guid.NewGuid()
               );

            var registry = new BuildRegistry {
                CommitHash = commit.commit.Hash,
                CommitDate = commit.commit.Date,
                Project = parameters.Project,
                Status = BuildStatus.Pending,
                Version = version,
            };
            await buildRegistry.Add(registry);

            await eventBus.PublisthAsync(command, ct);

            logger.LogInformation("Event published: {Commit} for {Type}", commit.commit.Hash, commit.branch.VersionType);

            if (state.LastSeenHashes.TryAdd(commit.branch.BranchName, commit.commit.Hash))
                state.LastSeenHashes[commit.branch.BranchName] = commit.commit.Hash;
        }

        branchRepository.Save(state);
    }
}



