using DiNet.GPipe.SharedKernel.Models;

namespace DiNet.GPipe.BuildingApplication.Apk;

public record ApkBuildCommand(string directory, BuildType type);
