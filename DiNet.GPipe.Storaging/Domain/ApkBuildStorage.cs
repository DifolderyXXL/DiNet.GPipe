using DiNet.GPipe.Storaging.Interfaces;
using DiNet.GPipe.Storaging.Settings;

namespace DiNet.GPipe.Storaging.Domain;

public class ApkBuildStorage(ApkStorageSettings settings) : IFileStorage
{
    private readonly ApkStorageSettings _settings = settings;

    public async Task StoreFrom(string filePath, CancellationToken token)
    {
        var fileName = Path.GetFileName(filePath);

        using var fromFs = File.OpenRead(filePath);

        using var toFs = new FileStream(Path.Combine(_settings.path, fileName), FileMode.OpenOrCreate);

        await fromFs.CopyToAsync(toFs, token);
    }
}
