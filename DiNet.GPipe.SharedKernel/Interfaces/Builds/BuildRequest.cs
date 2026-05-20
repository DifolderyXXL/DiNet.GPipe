namespace DiNet.GPipe.Infrastructure.Building.V2;

public record BuildRequest(
    int ProjectId,
    string RepositoryUrl,
    string CommitHash,
    string ProjectName,
    string BranchName);
