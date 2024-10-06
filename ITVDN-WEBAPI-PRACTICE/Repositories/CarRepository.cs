using ITVDN_WEBAPI_PRACTICE.Context;
using ITVDN_WEBAPI_PRACTICE.Models;
using Microsoft.EntityFrameworkCore;

namespace ITVDN_WEBAPI_PRACTICE.Repositories;

public class CarRepository : IRepository<Car>
{
    #region FIELDS
    private CarContext db;
    private bool disposed = false;
    #endregion

    #region CONSTUCTOR
    public CarRepository(CarContext db) => this.db = db;
    #endregion

    #region METHODS

    /// <summary>
    /// Метод, получение коллекцию объекта типа Car
    /// </summary>
    /// <returns>Возвращает Коллекцию объекта типа Car</returns>
    public IEnumerable<Car> GetAllCars() => db.Cars.ToList();


    /// <summary>
    /// Метод, получение объекта типа Car по id
    /// </summary>
    /// <param name="id">id объекта типа Car</param>
    /// <returns>Возвращает объект типа Car</returns>
    public Car GetCar(int id)
    {
        var car = db.Cars.FirstOrDefault(x=>x.Id == id);

        if (car == null)
            return null!;
        return car;
    }

    /// <summary>
    /// Метод, создание объекта типа Car, а также добавление в таблицу Cars из базы данных
    /// </summary>
    /// <param name="car">car, это объект типа Car</param>
    public void CreateCar(Car car)
    {
        db.Cars.Add(car);
        db.SaveChanges();
    }


    /// <summary>
    /// Метод, изменение объекта типа Car, а также сохранение этого объекта в таблицу Cars из базы данных
    /// </summary>
    /// <param name="car"></param>
    public void UpdateCar(Car car)
    {
        db.Entry(car).State = EntityState.Modified;
        db.SaveChanges();
    }


    /// <summary>
    /// Метод, удаление объекта типа Car через id объекта, а также изменение в таблице Cars из баз данных
    /// </summary>
    /// <param name="id">id объекта типа Car</param>
    public void DeleteCar(int id)
    {
        var car = db.Cars.FirstOrDefault(x=>x.Id==id);
        if(car != null)
        {
            db.Cars.Remove(car);
            db.SaveChanges();
        }
    }

    /// <summary>
    /// Виртуальный метод, для удаление контекст базы данных
    /// </summary>
    /// <param name="disposing">для вызова в контексте базы данных метод, Dispose в интерфейсе IDisposable</param>
    protected virtual void Dispose(bool disposing)
    {
        if(!this.disposed)
        {
            if(disposing)
            {
                db.Dispose();
            }
        }
        this.disposed = true;
    }

    /// <summary>
    /// Метод, для вызова виртуального метода Dispose, а также вызов Сборщика мусора (Garbage Collector)
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    #endregion
}