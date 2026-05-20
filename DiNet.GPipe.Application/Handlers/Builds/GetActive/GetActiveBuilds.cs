using DiNet.GPipe.Application.Handlers.Abstraction;

namespace DiNet.GPipe.Application.Handlers.Builds.GetActive;

public record GetActiveBuildsQuery : IQuery<List<ActiveBuildResponse>>;
