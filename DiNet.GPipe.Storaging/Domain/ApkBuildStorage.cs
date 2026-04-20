using DiNet.GPipe.Storaging.Interfaces;
using DiNet.GPipe.Storaging.Settings;

namespace DiNet.GPipe.Storaging.Domain;

public class ApkBuildStorage(ApkStorageSettings settings) : IFileStorage
{
    private readonly ApkStorageSettings _settings = settings;

    public async Task StoreFrom(string filePath, CancellationToken token)
    {
        EnsureLocalDirectoryExists();

        var fileName = GetTargetFileName(Path.GetFileName(filePath));

        using var fromFs = File.OpenRead(filePath);

        using var toFs = new FileStream(Path.Combine(_settings.path, fileName), FileMode.OpenOrCreate);

        await fromFs.CopyToAsync(toFs, token);
    }

    private string GetTargetFileName(string fileName)
    {
        if(settings.NamingSettings.addDateTime)
        {
            fileName += $"_{DateTime.UtcNow:yyyy-MM-dd_HH-mm-ss}";
        }

        if (settings.NamingSettings.numerate)
        {
            fileName += $"_cnt-{GetFileCount()+1}";
        }

        return fileName;
    }

    private int GetFileCount()
    {
        return Directory.GetFiles(_settings.path, "*", SearchOption.TopDirectoryOnly).Length;
    }

    private void EnsureLocalDirectoryExists()
    {
        Directory.CreateDirectory(_settings.path);
    }
}
