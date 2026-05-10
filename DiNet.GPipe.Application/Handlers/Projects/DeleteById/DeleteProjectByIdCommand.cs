using DiNet.GPipe.Application.Handlers.Abstraction;

namespace DiNet.GPipe.Application.Handlers.Projects.DeleteById;

public record DeleteProjectByIdCommand(int ProjectId) : ICommand;
