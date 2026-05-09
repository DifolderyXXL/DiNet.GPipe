namespace DiNet.GPipe.Domain;

public class TestEntry
{
    public int Id { get; set; }

    public int CommitId { get; private set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public bool CompletedSuccessfully { get; set; }
    public string ErrorText { get; set; } = null!;
}