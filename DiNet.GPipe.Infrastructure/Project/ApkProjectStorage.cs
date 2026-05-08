using Microsoft.Extensions.Options;
using DiNet.GPipe.BuildingApplication.Apk;
using DiNet.GPipe.SharedKernel;

namespace DiNet.GPipe.BuildingApplication.Handlers;

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
