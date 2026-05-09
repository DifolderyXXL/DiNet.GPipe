namespace DiNet.GPipe.Domain;

public class FailedBuild : Build
{
    public string ErrorText { get; set; } = null!;
}
