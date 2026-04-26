using DiNet.GPipe.BackgroundWorker.Versioning;
using DiNet.GPipe.Domain;
using DiNet.GPipe.SharedKernel.Interfaces;
using DiNet.GPipe.SharedKernel.Interfaces.Messaging;
using DiNet.GPipe.SharedKernel.Models;
using DiNet.GPipe.SharedKernel.Models.Commands;

namespace DiNet.GPipe.BackgroundWorker.Build;

public class BuildCommandHandler(IBuildRegistryRepository buildRepository,
                          IApkStagingStorage storage,
                          IWorkingDirectoryWorkspace workspace,
                          IEventBus eventBus,
                          IsolatedSpaceBuilder isolatedSpaceBuilder) : IAsyncEventHandler<CommitDetected>
{
    private readonly SemaphoreSlim _lock = new(1, 1);

    public async Task HandleAsync(CommitDetected command, CancellationToken ct)
    {
        await _lock.WaitAsync();

        try
        {
            await buildRepository.UpdateStatus(command.CommitHash, BuildStatus.Building);

            var result = await isolatedSpaceBuilder.BuildIsolated(
                workspace.WorkingDirectory,
                new BuildCommand(command.BranchName, command.CommitHash),
                ct
                );

            if (result.IsError)
            {
                await buildRepository.CreateFailedRecord(null, command.CommitHash, result.Error!.ToString());
                return;
            }

            var apk = await storage.Store(result.Value!, BuildType.Release, command.CommitHash, ct);

            await buildRepository.UpdateStatus(command.CommitHash, BuildStatus.Success);

            await eventBus.PublisthAsync(new ApkBuildSuccessful(apk.FilePath, command), ct);
        }
        catch (Exception ex)
        {
            await buildRepository.CreateFailedRecord(null, command.CommitHash, ex.Message);
        }
        finally
        {
            _lock.Release();
        }
    }
}