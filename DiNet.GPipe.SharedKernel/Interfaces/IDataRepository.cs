namespace DiNet.GPipe.BackgroundWorker.Branches;

public interface IDataRepository<T>
{
    public void Save(T value);
    public T? Get();
}
