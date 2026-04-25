using DiNet.GPipe.SharedKernel;
using DiNet.GPipe.SharedKernel.Models;

namespace DiNet.GPipe.BackgroundWorker.Branches;

public record CommitDetected(
    string ProjectName,
    string BranchName,
    string CommitHash,
    BuildVersion Version,
    Guid CorrelationId);
