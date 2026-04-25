using DiNet.GPipe.BackgroundWorker.Branches;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace DiNet.GPipe.Infrastructure.DataRepositories;

public class FileDataRepository<T>(IOptions<StorageSettings> settings) : IDataRepository<T>
{
    private readonly string _filePath = Path.Combine(
        settings.Value.BasePath,
        $"{typeof(T).Name}.json"
    );

    public T? Get()
    {
        if (!File.Exists(_filePath)) return default;

        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<T>(json);
    }

    public void Save(T value)
    {
        var json = JsonSerializer.Serialize(value);
        File.WriteAllText(_filePath, json);
    }
}
