using ClassLibrary.Data.Context;
using ClassLibrary.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary.Data.Repositories;

public class PitStopRepository : IRepository<PitStop>
{
    #region FIELDS
    private WebServiceContext _dbContext;
    private bool _disposed = false;
    #endregion

    #region CONSTRUCTOR
    public PitStopRepository(WebServiceContext dbContext) => this._dbContext = dbContext;
    #endregion


    #region METHODS

    /// <summary>
    /// Метод получение коллекция объекта типа PitStop
    /// </summary>
    /// <returns>Возвращает коллекция объекта типа PitStop</returns>
    public IEnumerable<PitStop> GetAll()
    {
        return _dbContext.PitStops
            .Include(x => x.Airport)
            .Include(x => x.DepartureAndArrival);
    }


    /// <summary>
    /// Метод получение объекта типа PitStop по id
    /// </summary>
    /// <param name="id">id объекта типа PitStop</param>
    /// <returns>Возвращает объект типа PitStop</returns>
    public PitStop GetById(int id)
    {
        PitStop? pitStop = _dbContext.PitStops
            .Include(x => x.Airport)
            .Include(x => x.DepartureAndArrival).FirstOrDefault(x => x.Id == id);

        if (pitStop == null)
            return null!;
        
        
        return pitStop;
    }


    /// <summary>
    /// Создание объекта типа PitStop, а также добавление в таблицу PitStops из базы данных
    /// </summary>
    /// <param name="pitStop">Объект типа PitStop</param>
    public void Create(PitStop pitStop)
    {
        _dbContext.PitStops.Add(pitStop);
        _dbContext.SaveChanges();
    }

    /// <summary>
    /// Метод, изменние объъекта типа PitStop, а также сохранение в таблицу PitStops из базы данных
    /// </summary>
    /// <param name="pitStop">Объект типа PitStop</param>
    public void Update(PitStop pitStop)
    {
        _dbContext.Entry(pitStop).State = EntityState.Modified;
        _dbContext.SaveChanges();
    }

    /// <summary>
    /// Метод удаление объекта типа PitStop, а также изменение в таблицы PitStops из базы данных
    /// </summary>
    /// <param name="id">id объекта типа PitStop</param>
    public void Delete(int id)
    {
        PitStop? pitStop = GetById(id);

        if(pitStop != null)
        {
            _dbContext.PitStops.Remove(pitStop);
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