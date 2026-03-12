namespace DiNet.GPipe.JavaBuilder;

public interface IApkBuilder
{
    public Task<string?> BuildAsync(CancellationToken token);
}