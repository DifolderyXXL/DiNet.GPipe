using DiNet.GPipe.BackgroundWorker.Common;

namespace DiNet.GPipe.BackgroundWorker.Versioning;

public interface IMetadataNamingService
{
    public bool IsValidName(string name);
    public string GetName(ApkMetadata metadata);
    public ApkMetadata? GetMetadata(string name);
}


