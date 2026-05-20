using DiNet.GPipe.Domain;
using DiNet.GPipe.SharedKernel.Interfaces;
using DiNet.GPipe.SharedKernel.Interfaces.Messaging;
using DiNet.GPipe.SharedKernel.Models.Commands;
using DiNet.GPipe.Infrastructure.Building;

namespace DiNet.GPipe.BuildingApplication.Handlers;


public record BuildCommand(
    string ProjectName,
    string BranchName,
    string RepositoryUrl,
    string CommitHash,
    BuildVersion Version,
    Guid CorrelationId);
// public class BuildCommandHandler(IBuildRegistryRepository buildRepository,
//                           IApkProjectStorage storage,
//                           IEventBus eventBus,
//                           IIsolatedBuilder isolatedSpaceBuilder) : IAsyncEventHandler<BuildCommand>
// {
//     private readonly SemaphoreSlim _lock = new(1, 1);

//     public async Task HandleAsync(BuildCommand command, CancellationToken ct)
//     {
//         await _lock.WaitAsync();

//         try
//         {
//             await buildRepository.UpdateStatus(command.CommitHash, BuildStatus.Building);

//             var result = await isolatedSpaceBuilder.BuildIsolated(
//                 command.RepositoryUrl,
//                 command.CommitHash,
//                 SharedKernel.Models.BuildType.Release,
//                 ct
//                 );

//             if (result.IsError)
//             {
//                 await buildRepository.CreateFailedRecord(null, command.CommitHash, result.Error!.ToString());
//                 return;
//             }

//             var apk = await storage.Store(result.Value!, command.ProjectName, command.CommitHash, ct);

//             await buildRepository.UpdateStatus(command.CommitHash, BuildStatus.Success);

//             await eventBus.PublishAsync(
//                 new ApkBuildSuccessful(
//                     apk.FilePath,
//                     command.ProjectName,
//                     command.BranchName,
//                     command.CommitHash,
//                     command.Version,
//                     command.CorrelationId),
//                 ct);
//         }
//         catch (Exception ex)
//         {
//             await buildRepository.CreateFailedRecord(null, command.CommitHash, ex.Message);
//         }
//         finally
//         {
//             _lock.Release();
//         }
//     }
// }