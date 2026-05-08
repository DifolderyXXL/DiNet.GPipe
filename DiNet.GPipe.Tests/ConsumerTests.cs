using Moq;

namespace DiNet.GPipe.Tests;

public class ConsumerTests
{
    private string TestingDirectory = Path.Join(Directory.GetCurrentDirectory(), "Testing");

    public ConsumerTests()
    {
        if (Directory.Exists(TestingDirectory))
            Directory.Delete(TestingDirectory, true);

        Directory.CreateDirectory(TestingDirectory);
    }



}