namespace DiNet.GPipe.Domain;

public abstract class Build
{
    public int Id { get; set; }

    public int CommitId { get; private set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
public class SuccessfullBuild : Build
{
    public string ApkUrl { get; set; } = null!;
}
