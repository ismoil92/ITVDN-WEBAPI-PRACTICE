namespace ITVDN_WEBAPI_PRACTICE.Repositories;

public interface IRepository<T> :IDisposable where T : class
{
    IEnumerable<T> GetAllBooks();
    T GetBookById(int id);
    void CreateBook(T book);
    void UpdateBook(T book);
    void DeleteBook(int id);
}