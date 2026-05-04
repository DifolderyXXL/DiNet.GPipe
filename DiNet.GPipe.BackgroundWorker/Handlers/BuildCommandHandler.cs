using DiNet.GPipe.BackgroundWorker.Apk;
using DiNet.GPipe.BackgroundWorker.Build;
using DiNet.GPipe.BackgroundWorker.Versioning;
using DiNet.GPipe.Domain;
using DiNet.GPipe.SharedKernel.Interfaces;
using DiNet.GPipe.SharedKernel.Interfaces.Messaging;
using DiNet.GPipe.SharedKernel.Models;
using DiNet.GPipe.SharedKernel.Models.Commands;
using Microsoft.Extensions.Options;

namespace DiNet.GPipe.BuildingApplication.Handlers;


public interface IApkProjectStorage 
{
    public Task<IApkFile> Store(IApkFile file, string projectName, string commitHash, CancellationToken ct);
}

public class ApkProjectStorage(IOptions<DirectoryWorkspaceOptions> workspace) : IApkProjectStorage
{
    public async Task<IApkFile> Store(IApkFile file, string projectName, string commitHash, CancellationToken ct)
    {
        var directory = Path.Join(workspace.Value.ProjectsApkDirectory, projectName);

        Directory.CreateDirectory(directory);

        var target = Path.Join(directory, $"{Path.GetFileNameWithoutExtension(file.FilePath)}_{commitHash}.apk");

        await file.MoveToAsync(target, ct);

        return file;
    }
}


public record BuildCommand(
    string ProjectName,
    string BranchName,
    string RepositoryUrl,
    string CommitHash,
    BuildVersion Version,
    Guid CorrelationId);
public class BuildCommandHandler(IBuildRegistryRepository buildRepository,
                          IApkProjectStorage storage,
                          IOptions<DirectoryWorkspaceOptions> workspace,
                          IEventBus eventBus,
                          IsolatedSpaceBuilder isolatedSpaceBuilder) : IAsyncEventHandler<BuildCommand>
{
    private readonly SemaphoreSlim _lock = new(1, 1);

    public async Task HandleAsync(BuildCommand command, CancellationToken ct)
    {
        await _lock.WaitAsync();

        try
        {
            await buildRepository.UpdateStatus(command.CommitHash, BuildStatus.Building);

            var result = await isolatedSpaceBuilder.BuildIsolated(
                command.RepositoryUrl,
                workspace.Value.WorkingDirectory,
                command.CommitHash,
                ct
                );

            if (result.IsError)
            {
                await buildRepository.CreateFailedRecord(null, command.CommitHash, result.Error!.ToString());
                return;
            }

            var apk = await storage.Store(result.Value!, command.ProjectName, command.CommitHash, ct);

            await buildRepository.UpdateStatus(command.CommitHash, BuildStatus.Success);

            await eventBus.PublisthAsync(
                new ApkBuildSuccessful(
                    apk.FilePath, 
                    command.ProjectName,
                    command.BranchName,
                    command.CommitHash,
                    command.Version,
                    command.CorrelationId), 
                ct);
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