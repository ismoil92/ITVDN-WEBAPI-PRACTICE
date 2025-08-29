using ClassLibrary.Data.Models;
using ClassLibrary.Data.Repositories;
using ClassLibrary.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary.Repositories;

public class AirportRepository : IRepository<Airport>
{
    #region FIELDS
    private WebServiceContext _dbContext;
    private bool _disposed = false;
    #endregion

    #region CONSTRUCTOR
    public AirportRepository(WebServiceContext dbContext) => this._dbContext = dbContext;
    #endregion

    #region METHODS


    /// <summary>
    /// Метод получение коллекция объекта типа Airport
    /// </summary>
    /// <returns>Возвращает коллекция объекта типа Airport</returns>
    public IEnumerable<Airport> GetAll()
    {
        return _dbContext.Airports
            .Include(x=>x.Information)
            .Include(x=>x.CodeAirports)
            .Include(x=>x.DepartureAndArrivals)
            .Include(x=>x.PitStops);
    }

    /// <summary>
    /// Метод получение объекта типа Airport по id
    /// </summary>
    /// <param name="id">id объекта типа Airport</param>
    /// <returns>Возвращает объект типа Airport</returns>
    public Airport GetById(int id)
    {
        Airport? airport = _dbContext.Airports
            .Include(x=>x.Information)
            .Include (x=>x.CodeAirports)
            .Include(x=>x.DepartureAndArrivals)
            .Include(x=>x.PitStops)
            .FirstOrDefault(x => x.Id == id);

        if(airport == null)
        {
            return null!;
        }
        return airport;
    }


    /// <summary>
    /// Создание объекта типа Airport, а также добавление в таблицу Airports из базы данных
    /// </summary>
    /// <param name="airport">Объект типа Airport</param>
    public void Create(Airport airport)
    {
        _dbContext.Airports.Add(airport);
        _dbContext.SaveChanges();
    }


    /// <summary>
    /// Метод, изменние объъекта типа Airport, а также сохранение в таблицу Airports из базы данных
    /// </summary>
    /// <param name="airport">Объект типа Airport</param>
    public void Update(Airport airport)
    {
        _dbContext.Entry(airport).State = EntityState.Modified;
        _dbContext.SaveChanges();
    }


    /// <summary>
    /// Метод удаление объекта типа Airport, а также изменение в таблицы Airports из базы данных
    /// </summary>
    /// <param name="id">id объекта типа Airport</param>
    public void Delete(int id)
    {
        Airport? airport = GetById(id);
        if(airport != null)
        {
            _dbContext.Airports.Remove(airport);
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
        _disposed = true;
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