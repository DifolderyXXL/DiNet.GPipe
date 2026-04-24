using DiNet.GPipe.BackgroundWorker.Common;

namespace DiNet.GPipe.BackgroundWorker.Apk;

public record ApkProvideCommand(string directory, BuildType type);


