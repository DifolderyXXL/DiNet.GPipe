using DiNet.GPipe.Application.Handlers.Abstraction;

namespace DiNet.GPipe.Application.Handlers.Projects.Get;

public record GetProjectsQuery : IQuery<List<ProjectResponse>>;
