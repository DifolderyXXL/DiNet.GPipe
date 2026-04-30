using DiNet.GPipe.SharedKernel.Results;

namespace DiNet.GPipe.Application.Project;

public static class ProjectErrors
{
    public static Error ProjectNotFounded() => new Error(nameof(ProjectNotFounded), ErrorType.NotFound);

    public static Error ProjectAlreadyExists(string gitUrl) => new Error(
        nameof(ProjectAlreadyExists), 
        $"Project for {gitUrl} exists.",
        ErrorType.NotFound);

    public static Error ProjectConflict() => new Error(
        nameof(ProjectConflict),
        ErrorType.Conflict);
}
