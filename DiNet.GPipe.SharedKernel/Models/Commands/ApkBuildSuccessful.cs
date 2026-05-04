using DiNet.GPipe.Domain;

namespace DiNet.GPipe.SharedKernel.Models.Commands;

public record ApkBuildSuccessful(
    string apkPath,
    string ProjectName,
    string BranchName,
    string CommitHash,
    BuildVersion Version,
    Guid CorrelationId
    );