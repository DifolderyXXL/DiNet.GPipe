using DiNet.GPipe.Application.Handlers.Messaging;
using DiNet.GPipe.SharedKernel.Interfaces;
using DiNet.GPipe.SharedKernel.Results;

namespace DiNet.GPipe.Application.Handlers.Projects.Get;

internal class GetProjects(IProjectsRepository repository) : IQueryHandler<GetProjectsQuery, List<ProjectResponse>>
{
    public async Task<Result<List<ProjectResponse>>> Handle(GetProjectsQuery query, CancellationToken ct)
    {
        return (await repository.QueryAll()).Select(x => new ProjectResponse(
            x.Id,
            x.Name,
            x.GitUrl,
            x.WatcherSettings.IsActive,
            x.WatcherSettings.PollInterval,
            [.. x.BranchConfigs.Select(y => new BranchDto(y.BranchName, y.VersionType))]
            )).ToList();
    }
}
