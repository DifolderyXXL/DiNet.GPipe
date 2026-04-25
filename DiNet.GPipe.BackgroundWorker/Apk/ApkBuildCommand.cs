using DiNet.GPipe.SharedKernel.Models;

namespace DiNet.GPipe.BackgroundWorker.Apk;

public record ApkBuildCommand(string directory, BuildType type);