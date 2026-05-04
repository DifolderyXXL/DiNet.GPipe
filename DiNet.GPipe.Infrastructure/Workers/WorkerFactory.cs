using DiNet.GPipe.Application.Workers;
using DiNet.GPipe.Domain;
using DiNet.GPipe.Infrastructure.Project;
using DiNet.GPipe.SharedKernel.Models;
using DiNet.GPipe.SharedKernel.Watchers;
using Microsoft.Extensions.DependencyInjection;
using DiNet.GPipe.SharedKernel.Interfaces;

namespace DiNet.GPipe.Infrastructure.Workers;

public class WorkerFactory(IServiceScopeFactory scopeFactory) : IWorkerFactory
{
    public WatcherEntry Create(ProjectModel project, WatcherParameters request)
    {
        var cts = new CancellationTokenSource();

        var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IProjectScopeContext>();
        context.Initialize(project.Id, request.Project.Name, request.Project.GitUrl);

        
        var workerInstance = ActivatorUtilities.CreateInstance<BranchCheckWorker>(scope.ServiceProvider, request);

        var watcher = new Watcher(project.Id, request.Project.Name, request.Project.GitUrl, request.Branches, WatcherStatus.Created);

        var entry = new WatcherEntry(cts, watcher, scope, workerInstance);

        return entry;
    }
}
