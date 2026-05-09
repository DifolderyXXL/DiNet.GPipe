using DiNet.GPipe.Application.Handlers.Abstraction;

namespace DiNet.GPipe.Application.Handlers.Commits.Get;

public record GetCommitsByProjectIdQuery(int projectId, bool includeActivity) : IQuery<List<CommitResponse>>;
