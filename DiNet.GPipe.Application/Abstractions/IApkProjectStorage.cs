using DiNet.GPipe.BuildingApplication.Apk;

namespace DiNet.GPipe.BuildingApplication.Handlers;

public interface IApkProjectStorage
{
    public Task<IApkFile> Store(IApkFile file, string projectName, string commitHash, CancellationToken ct);
    public Task<IApkFile?> GetApkFileAsync(string projectName, string commitHash, CancellationToken ct);
}
