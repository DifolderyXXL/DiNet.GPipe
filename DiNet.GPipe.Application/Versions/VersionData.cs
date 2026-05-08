using DiNet.GPipe.Domain;

namespace DiNet.GPipe.Application.Versions;

public class VersionData
{
    public BuildVersion Latest { get; set; } = new BuildVersion(0, 0, 0);
}

