using DiNet.GPipe.Domain;
using DiNet.GPipe.Infrastructure.Project;
using DiNet.GPipe.SharedKernel.Watchers;
using Microsoft.Extensions.DependencyInjection;

namespace DiNet.GPipe.Application.Workers;

public class WorkerFactory(IServiceScopeFactory scopeFactory) : IWorkerFactory
{
    public WatcherEntry Create(ProjectModel project, WatcherRequest request)
    {
        var id = Guid.NewGuid();
        var cts = new CancellationTokenSource();

        var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IProjectScopeContext>();
        context.Initialize(project.Id, request.ProjectName, request.GitUrl);


        var parameters = new WatcherParameters(project, request.Branches, request.PollInterval);
        var workerInstance = ActivatorUtilities.CreateInstance<BranchCheckWorker>(scope.ServiceProvider, parameters);

        var watcher = new Watcher(id, request.ProjectName, request.GitUrl, request.Branches, WatcherStatus.Created);

        var entry = new WatcherEntry(cts, watcher, scope, workerInstance);

        return entry;
    }
}
