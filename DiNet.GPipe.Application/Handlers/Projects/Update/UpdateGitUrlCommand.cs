using DiNet.GPipe.Application.Handlers.Abstraction;

namespace DiNet.GPipe.Application.Handlers.Projects.Update;

public record UpdateGitUrlCommand(int ProjectId, string NewGitUrl) : ICommand;
