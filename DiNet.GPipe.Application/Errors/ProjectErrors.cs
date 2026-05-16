using DiNet.GPipe.SharedKernel.Results;

namespace DiNet.GPipe.Application.Project;

public static class ProjectErrors
{
    public static Error ProjectNotFound() => new Error(nameof(ProjectNotFound), ErrorType.NotFound);

    public static Error ProjectAlreadyExists(string gitUrl) => new Error(
        nameof(ProjectAlreadyExists), 
        $"Project for {gitUrl} exists.",
        ErrorType.Conflict);

    public static Error ProjectConflict() => new Error(
        nameof(ProjectConflict),
        ErrorType.Conflict);
}

public static class BranchErrors
{
        public static Error BranchNotFound() 
            => new Error(nameof(BranchNotFound), ErrorType.NotFound);

}