using DiNet.GPipe.SharedKernel.Results;

namespace DiNet.GPipe.BackgroundWorker.Apk;

public interface IApkBuilder
{
    public Task<Result<IApkFile>> Build(ApkBuildCommand command, CancellationToken cancellationToken = default);
}
