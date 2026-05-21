namespace DiNet.GPipe.Dashboard.Models;

public interface IState<T>
{
    public T? Selected { get; }

    public event Action OnChange;

    void Select(T? state);
}

public class GenericState<T> : IState<T>
{
    public T? Selected { get; private set; }

    public event Action? OnChange;

    public void Select(T? state)
    {
        Selected = state;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}