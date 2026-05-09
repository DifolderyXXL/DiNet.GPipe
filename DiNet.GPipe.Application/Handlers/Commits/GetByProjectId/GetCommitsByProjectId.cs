using DiNet.GPipe.Application.Handlers.Abstraction;
using DiNet.GPipe.SharedKernel.Interfaces;
using DiNet.GPipe.SharedKernel.Results;

namespace DiNet.GPipe.Application.Handlers.Commits.Get;
internal class GetCommitsByProjectId(ICommitsRepository repository) : IQueryHandler<GetCommitsByProjectIdQuery, List<CommitResponse>>
{
    public async Task<Result<List<CommitResponse>>> Handle(GetCommitsByProjectIdQuery query, CancellationToken ct)
    {
        var result = await (query.includeActivity
            ? repository.QueryWithActivity(query.projectId)
            : repository.QueryWithoutActivity(query.projectId));

        return result.Select(x=>new CommitResponse()
        {
            Id = x.Id,
            ProjectId = x.ProjectId,
            Hash = x.Hash,
            Name = x.Name,
            Date = x.Date,
            BuildVersion = x.BuildVersion,
            SuccessfullBuilds = x.SuccessfullBuilds,
            FailedBuilds = x.FailedBuilds,
            TestEntries = x.TestEntries
        }).ToList();

    }
}
