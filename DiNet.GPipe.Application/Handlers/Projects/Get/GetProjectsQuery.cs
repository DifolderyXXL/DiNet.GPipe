using DiNet.GPipe.Application.Handlers.Messaging;

namespace DiNet.GPipe.Application.Handlers.Projects.Get;

public record GetProjectsQuery : IQuery<List<ProjectResponse>>;
