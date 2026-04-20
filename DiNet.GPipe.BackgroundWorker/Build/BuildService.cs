using DiNet.GPipe.BackgroundWorker.Apk;
using DiNet.GPipe.BackgroundWorker.Common;
using DiNet.GPipe.BackgroundWorker.Versioning;

namespace DiNet.GPipe.BackgroundWorker.Build;


public interface IBuildService
{
    public Task Build(string branch, string commitHash, BranchVersion versionType, CancellationToken cancellationToken);
}
public class BuildService(IBuildRepository buildRepository,
                          IApkStagingStorage storage,
                          IApkProviderApi api,
                          IVersionService versionService) : IBuildService
{
    private readonly SemaphoreSlim _lock = new(1, 1);
    public async Task Build(string branch, string commitHash, BranchVersion versionType, CancellationToken cancellationToken)
    {
        await _lock.WaitAsync();

        try
        {
            var apk = await api.Provide(new ApkProvideCommand(BuildType.Release));

            apk = await storage.Store(apk, BuildType.Release, commitHash, cancellationToken);

            var version = versionService.Put(versionType);

            var rec = new BuildRecord()
            {
                CommitHash = commitHash,
                Status = BuildStatus.Success,
                BuildPath = apk.FilePath,
                Version = version
            };

            await buildRepository.Save(rec);
        }
        catch (Exception ex)
        {
            await buildRepository.CreateFailedRecord(null, commitHash, ex.Message);
        }
        finally
        {
            _lock.Release();
        }
    }
}