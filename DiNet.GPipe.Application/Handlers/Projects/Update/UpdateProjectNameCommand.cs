using DiNet.GPipe.Application.Handlers.Abstraction;

namespace DiNet.GPipe.Application.Handlers.Projects.Update;

public record UpdateProjectNameCommand(int ProjectId, string NewName) : ICommand;
