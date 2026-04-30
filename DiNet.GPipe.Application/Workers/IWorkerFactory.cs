using DiNet.GPipe.Domain;
using DiNet.GPipe.SharedKernel.Models;
using DiNet.GPipe.SharedKernel.Watchers;

namespace DiNet.GPipe.Application.Workers;

public interface IWorkerFactory
{
    public WatcherEntry Create(ProjectModel project, WatcherParameters request);
}
