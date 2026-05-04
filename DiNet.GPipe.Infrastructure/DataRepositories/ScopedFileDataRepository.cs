using DiNet.GPipe.Infrastructure.Project;
using Microsoft.Extensions.Options;
using System.Text.Json;
using DiNet.GPipe.SharedKernel.Interfaces;

namespace DiNet.GPipe.Infrastructure.DataRepositories;

public class ScopedFileDataRepository<T>(IProjectScopeContext context, IOptions<ScopedStorageOptions> settings) : IDataRepository<T>
{
    private readonly string _filePath = Path.Combine(
        settings.Value.BasePath,
        context.ProjectName,
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
        Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);

        var json = JsonSerializer.Serialize(value);
        File.WriteAllText(_filePath, json);
    }
}
