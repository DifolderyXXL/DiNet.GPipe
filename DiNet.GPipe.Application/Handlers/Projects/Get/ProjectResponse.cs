namespace DiNet.GPipe.Application.Handlers.Projects.Get;

public record ProjectResponse(
    int Id,
    string Name,
    string GitUrl,
    bool IsActive,
    TimeSpan PollInterval,
    List<BranchDto> Branches
    );
