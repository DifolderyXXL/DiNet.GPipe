using DiNet.GPipe.Application.Handlers.Projects.Get;

namespace DiNet.GPipe.Dashboard.Models;

public class AppState
{
    public ProjectResponse? SelectedProject { get; private set; }

    public event Action? OnChange;

    public void SelectProject(ProjectResponse? project)
    {
        SelectedProject = project;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
