namespace DiNet.GPipe.Storaging.Interfaces;

public interface IFileStorage
{
    public Task StoreFrom(string filePath, CancellationToken token);
}

