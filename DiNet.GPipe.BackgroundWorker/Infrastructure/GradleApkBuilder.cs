using DiNet.GPipe.BackgroundWorker.Apk;
using DiNet.GPipe.BackgroundWorker.Git;
using DiNet.GPipe.JavaBuilder;
using DiNet.GPipe.JavaBuilder.Domain;
using DiNet.GPipe.JavaBuilder.Settings;
using DiNet.GPipe.SharedKernel.Results;
using LibGit2Sharp;

namespace DiNet.GPipe.BuildingApplication.Infrastructure;

public class GradleApkBuilder(JdkSettings jdkSettings) : IApkBuilder
{
    public async Task<Result<IApkFile>> Build(ApkBuildCommand command, CancellationToken cancellationToken = default)
    {
        var androidStudioBuilder = new AndroidStudioApkBuilder(jdkSettings);

        var apk = await androidStudioBuilder.BuildAsync(command.directory,
            command.type switch
            {
                SharedKernel.Models.BuildType.Debug => ApkBuildType.Debug,
                SharedKernel.Models.BuildType.Release => ApkBuildType.Release,
                _ => throw new NotImplementedException()
            },
            cancellationToken);

        return
            apk == null ? null :
            new SystemApkFile(apk);
    }
}