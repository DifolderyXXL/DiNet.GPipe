using DiNet.GPipe.Domain;
using DiNet.GPipe.SharedKernel.Watchers;

namespace DiNet.GPipe.Application.Workers;

public interface IWorkerFactory
{
    public WatcherEntry Create(ProjectModel project, WatcherRequest request);
}
