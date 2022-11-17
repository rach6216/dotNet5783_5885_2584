
namespace DalApi;

public interface ICrud<T>
{
    public int Create(T entity);
    public void Delete(int id);
    public void Update(T entity);
    public IEnumerable<T> Read();
    public T Read(int id);
}
