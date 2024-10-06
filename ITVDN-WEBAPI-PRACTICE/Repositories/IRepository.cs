namespace ITVDN_WEBAPI_PRACTICE.Repositories;

public interface IRepository<T> :IDisposable where T : class
{
    IEnumerable<T> GetAllCars();
    T GetCar(int id);
    void CreateCar(T car);
    void UpdateCar(T car);
    void DeleteCar(int id);
}