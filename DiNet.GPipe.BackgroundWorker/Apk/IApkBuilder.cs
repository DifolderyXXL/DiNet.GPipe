using DiNet.GPipe.SharedKernel;

namespace DiNet.GPipe.BackgroundWorker.Apk;

public interface IApkBuilder
{
    public Task<Result<IApkFile>> Provide(ApkProvideCommand command, CancellationToken cancellationToken = default);
}
