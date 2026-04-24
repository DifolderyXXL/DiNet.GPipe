using DiNet.GPipe.BackgroundWorker.Apk;
using DiNet.GPipe.BackgroundWorker.Common;
using DiNet.GPipe.BackgroundWorker.Versioning;

namespace DiNet.GPipe.BackgroundWorker.Build;


public interface IHandleCommitVersionUpdate
{
    public Task Build(string branch, string commitHash, BranchVersion versionType, CancellationToken cancellationToken);
}
public class BuildService(IBuildRepository buildRepository,
                          IApkStagingStorage storage,
                          IWorkingDirectoryWorkspace workspace,
                          IsolatedSpaceBuilder isolatedSpaceBuilder,
                          IVersionService versionService) : IHandleCommitVersionUpdate
{
    private readonly SemaphoreSlim _lock = new(1, 1);
    public async Task Build(string branch, string commitHash, BranchVersion versionType, CancellationToken cancellationToken)
    {
        await _lock.WaitAsync();

        try
        {
            var result = await isolatedSpaceBuilder.BuildIsolated(
                workspace.WorkingDirectory, 
                new BuildCommand(branch, commitHash), 
                cancellationToken
                );

            if (result.IsError)
            {
                await buildRepository.CreateFailedRecord(null, commitHash, result.Error!.ToString());
                return;
            }

            var apk = await storage.Store(result.Value!, BuildType.Release, commitHash, cancellationToken);

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