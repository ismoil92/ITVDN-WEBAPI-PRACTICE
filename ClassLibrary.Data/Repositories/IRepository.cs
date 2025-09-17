namespace ClassLibrary.Data.Repositories;

public interface IRepository<T> :IDisposable where T : class
{
    public IEnumerable<T> GetAll();
    T FindById(int id);
    void Create(T entity);
    void Update(T entity);
    void Delete(int id);
}