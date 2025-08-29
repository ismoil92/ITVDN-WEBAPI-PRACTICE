namespace ClassLibrary.Data.Repositories;

public interface IRepository<T> :IDisposable where T : class
{
    IEnumerable<T> GetAll();
    T GetById(int id);
    void Create(T t);
    void Update(T t);
    void Delete(int id);

}