using DiNet.GPipe.Domain;

namespace DiNet.GPipe.SharedKernel.Models.Commands;

public record CommitDetected(
    string ProjectName,
    string BranchName,
    string CommitHash,
    BuildVersion Version,
    Guid CorrelationId);
