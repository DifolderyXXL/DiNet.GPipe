using DiNet.GPipe.Domain;

namespace DiNet.GPipe.SharedKernel.Models.Commands;

public record CommitDetected(
    string ProjectName,
    int ProjectId,
    string BranchName,
    string CommitHash,
    string CommitName,
    DateTime CommitDate,
    BuildVersion Version,
    Guid CorrelationId);
