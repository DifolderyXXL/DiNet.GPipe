namespace DiNet.GPipe.SharedKernel.Models.Commands;

public record ApkBuildSuccessful(
    string apkPath,
    CommitDetected commit
    );