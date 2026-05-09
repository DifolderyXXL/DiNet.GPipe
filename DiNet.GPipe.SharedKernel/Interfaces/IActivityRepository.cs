using DiNet.GPipe.Domain;

namespace DiNet.GPipe.Infrastructure.Database;

public interface IActivityRepository
{
    Task AddBuild(Build build);
    Task AddTest(TestEntry test);
}