using Microsoft.Extensions.Options;
using DiNet.GPipe.BuildingApplication.Apk;
using DiNet.GPipe.SharedKernel;

namespace DiNet.GPipe.BuildingApplication.Handlers;

public class ApkProjectStorage(IOptions<DirectoryWorkspaceOptions> workspace) : IApkProjectStorage
{
    public Task<IApkFile?> GetApkFileAsync(string projectName, string commitHash, CancellationToken ct)
    {
        var directory = Path.Join(workspace.Value.ProjectsApkDirectory, projectName);
        var target = Path.Join(directory, $"{commitHash}.apk");

        return Task.FromResult<IApkFile?>(new SystemApkFile(target));
    }

    public async Task<IApkFile> Store(IApkFile file, string projectName, string commitHash, CancellationToken ct)
    {
        var directory = Path.Join(workspace.Value.ProjectsApkDirectory, projectName);

        Directory.CreateDirectory(directory);

        var target = Path.Join(directory, $"{commitHash}.apk");

        await file.MoveToAsync(target, ct);

        return file;
    }
}
