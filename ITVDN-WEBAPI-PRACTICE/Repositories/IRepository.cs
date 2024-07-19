namespace ITVDN_WEBAPI_PRACTICE.Repositories;

public interface IRepository<T> : IDisposable where T : class
{
    IEnumerable<T> GetPeople();
    T GetPerson(int id);
    void CreatePerson(T person);
    void UpdatePerson(T person);
    void DeletePerson(int id);

}