namespace DiNet.GPipe.Dashboard.Models;

public class AppState
{
    public ProjectState? SelectedProject { get; private set; }

    public event Action? OnChange;

    public void SelectProject(ProjectState? project)
    {
        SelectedProject = project;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
