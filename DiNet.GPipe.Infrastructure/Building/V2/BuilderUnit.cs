using DiNet.GPipe.BuildingApplication.Apk;
using DiNet.GPipe.SharedKernel.Interfaces;
using DiNet.GPipe.SharedKernel.Interfaces.Logging;
using DiNet.GPipe.SharedKernel.Results;

namespace DiNet.GPipe.Infrastructure.Building.V2;

public class BuilderUnit(IBuildRegistryRepository buildRepository,
                          IIsolatedBuilder isolatedSpaceBuilder) : IBuilderUnit
{

    public async Task<Result<IApkFile>> RunAsync(BuildRequest request, IProcessLogger logger, CancellationToken ct)
    {
        logger.Log($"Starting build process for project: {request.ProjectName} on branch {request.BranchName}");

        try
        {
            var result = await isolatedSpaceBuilder.BuildIsolated(
               request.RepositoryUrl,
               request.CommitHash,
               SharedKernel.Models.BuildType.Release,
               logger,
               ct
               );

            if (result.IsError)
            {
                logger.Log($"Build failed: {result.Error}");
                await buildRepository.CreateFailedRecord(null, request.CommitHash, result.Error!.ToString());

                return result;
            }

            logger.Log("Build completed successfully.");
            return result;
        }
        catch (Exception ex)
        {
            logger.Log($"Critical error during execution: {ex.Message}");

            await buildRepository.CreateFailedRecord(null, request.CommitHash, ex.Message);

            return new Error("BuilderUnitError", ex.Message, ErrorType.Internal);
        }
    }
}
