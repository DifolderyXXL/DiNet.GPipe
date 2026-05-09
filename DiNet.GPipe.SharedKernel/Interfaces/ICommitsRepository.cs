using DiNet.GPipe.Domain;

namespace DiNet.GPipe.SharedKernel.Interfaces;

public interface ICommitsRepository
{
    Task<List<CommitEntry>> QueryWithActivity();
    Task<List<CommitEntry>> QueryWithoutActivity();

    Task<List<CommitEntry>> QueryWithActivity(int projectId);
    Task<List<CommitEntry>> QueryWithoutActivity(int projectId);

    Task Add(CommitEntry commitEntry);
}