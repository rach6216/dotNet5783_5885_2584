


namespace DalApi;
/// <summary>
/// Interface for basis actions of data
/// </summary>
/// <typeparam name="T">type of data</typeparam>
public interface ICrud<T>where T:struct
{
    /// <summary>
    /// create new data
    /// </summary>
    /// <param name="entity"> data tp add</param>
    /// <returns>id of the new entity</returns>
    public int Create(T entity);
    /// <summary>
    /// delete entity by id
    /// </summary>
    /// <param name="id">data id to delete</param>
    public void Delete(int id);
    /// <summary>
    /// update entity
    /// </summary>
    /// <param name="entity">the entity details</param>
    public void Update(T entity);
    /// <summary>
    /// get collection with option to Enumerator 
    /// </summary>
    /// <returns>collection type T</returns>
    public IEnumerable<T?> ReadAll(Func<T?, bool>? f = null);
    /// <summary>
    /// get specific item by id
    /// </summary>
    /// <param name="id">entity id</param>
    /// <returns></returns>
    public T Read(Func<T?, bool> f);
}
