namespace DiNet.GPipe.Domain;

public class CommitEntry
{
    public int Id { get; set; }

    public int ProjectId { get; set; }
    public string Hash { get; set; } = null!;
    public string Name { get; set; } = null!;
    public DateTime Date { get; set; }
    public BuildVersion BuildVersion { get; set; } = null!;


    public List<SuccessfullBuild> SuccessfullBuilds { get; set; } = [];
    public List<FailedBuild> FailedBuilds { get; set; } = [];
    public List<TestEntry> TestEntries { get; set; } = [];
}
