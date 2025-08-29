using ClassLibrary.Data.Context;
using ClassLibrary.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary.Data.Repositories;

public class DepartureAndArrivalRepository : IRepository<DepartureAndArrival>
{
    #region FIELDS
    private WebServiceContext _dbContext;
    private bool _disposed = false;
    #endregion

    #region CONSTRUCTOR
    public DepartureAndArrivalRepository(WebServiceContext dbContext) => this._dbContext = dbContext;

    #endregion


    #region METHODS

    /// <summary>
    /// Метод получение коллекция объекта типа DepartureAndArrival
    /// </summary>
    /// <returns>Возвращает коллекция объекта типа DepartureAndArrival</returns>
    public IEnumerable<DepartureAndArrival> GetAll()
    {
        return _dbContext.DeparturesAndArrivals
            .Include(x => x.Airport)
            .Include(x => x.PitStops);
    }


    /// <summary>
    /// Метод получение объекта типа DepartureAndArrival по id
    /// </summary>
    /// <param name="id">id объекта типа DepartureAndArrival</param>
    /// <returns>Возвращает объект типа DepartureAndArrival</returns>
    public DepartureAndArrival GetById(int id)
    {
        DepartureAndArrival? departureAndArrival = _dbContext.DeparturesAndArrivals
            .Include(x => x.Airport)
            .Include(x => x.PitStops)
            .FirstOrDefault(x => x.Id == id);

        if (departureAndArrival == null)
            return null!;

        return departureAndArrival;
    }

    /// <summary>
    /// Создание объекта типа DepartureAndArrival, а также добавление в таблицу DeparturesAndArrivals из базы данных
    /// </summary>
    /// <param name="departureAndArrival">Объект типа DepartureAndArrival</param>
    public void Create(DepartureAndArrival departureAndArrival)
    {
        _dbContext.DeparturesAndArrivals.Add(departureAndArrival);
        _dbContext.SaveChanges();
    }


    /// <summary>
    /// Метод, изменние объъекта типа DepartureAndArrival, а также сохранение в таблицу DeparturesAndArrivals из базы данных
    /// </summary>
    /// <param name="departureAndArrival">Объект типа DepartureAndArrival</param>
    public void Update(DepartureAndArrival departureAndArrival)
    {
        _dbContext.Entry(departureAndArrival).State = EntityState.Modified;
        _dbContext.SaveChanges();
    }


    /// <summary>
    /// Метод удаление объекта типа DepartureAndArrival, а также изменение в таблицы DeparturesAndArrivals из базы данных
    /// </summary>
    /// <param name="id">id объекта типа DepartureAndArrival</param>
    public void Delete(int id)
    {
        DepartureAndArrival? departureAndArrival = GetById(id);
        if(departureAndArrival != null)
        {
            _dbContext.DeparturesAndArrivals.Remove(departureAndArrival);
            _dbContext.SaveChanges();
        }
    }

    /// <summary>
    /// Виртуальный метод, для удаление контекст базы данных
    /// </summary>
    /// <param name="disposing">Для вызова в контексте базы данных метод,Dispose в интерфейсе IDisposable</param>
    protected virtual void Dispose(bool disposing)
    {
        if(!this._disposed)
        {
            if(disposing)
            {
                _dbContext.Dispose();
            }
        }
        this._disposed = true;
    }

    /// <summary>
    /// Метод для вызова виртуального метода Dispose, а также вызов сборщика мусора (Garbage Collector)
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    #endregion
}