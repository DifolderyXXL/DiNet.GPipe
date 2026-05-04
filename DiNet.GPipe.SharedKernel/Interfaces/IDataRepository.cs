namespace DiNet.GPipe.SharedKernel.Interfaces;

public interface IDataRepository<T>
{
    public void Save(T value);
    public T? Get();
}
