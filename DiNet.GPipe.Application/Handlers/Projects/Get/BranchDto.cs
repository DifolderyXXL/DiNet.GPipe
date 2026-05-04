using DiNet.GPipe.Domain;

namespace DiNet.GPipe.Application.Handlers.Projects.Get;

public record BranchDto(
    string Name,
    BranchVersion version
    );
