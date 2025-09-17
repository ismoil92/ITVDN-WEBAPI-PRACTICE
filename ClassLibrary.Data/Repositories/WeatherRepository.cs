using ClassLibrary.Data.Context;
using ClassLibrary.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary.Data.Repositories;

public class WeatherRepository : IRepository<Weather>
{
    #region FIELDS
    private WeatherContext _dbContext;
    private bool _disposed = false;
    #endregion

    #region CONSTRUCTOR
    public WeatherRepository(WeatherContext dbContext) => this._dbContext = dbContext;
    #endregion


    #region METHODS
    /// <summary>
    /// Метод получение коллекция объекта типа Weather
    /// </summary>
    /// <returns>Возвращает коллекция объекта типа Weather</returns>
    public IEnumerable<Weather> GetAll() => _dbContext.Weathers;


    /// <summary>
    /// Метод для нахождение объекта типа Weather по id
    /// </summary>
    /// <param name="id">id объекта типа Weather</param>
    /// <returns>Возвращает объект типа Weather</returns>
    public Weather FindById(int id)
    {
        Weather? weather = _dbContext.Weathers.FirstOrDefault(x => x.Id == id);
        if (weather is null)
            return null!;

        return weather;
    }



    /// <summary>
    /// Создание объекта типа Weather, а также добавление в таблицу Weathers из базы данных
    /// </summary>
    /// <param name="weather">Объект типа Weather</param>
    public void Create(Weather weather)
    {
        _dbContext.Weathers.Add(weather);
        _dbContext.SaveChanges();
    }


    /// <summary>
    /// Метод, изменние объъекта типа Weather, а также сохранение в таблицу Weathers из базы данных
    /// </summary>
    /// <param name="weather">Объект типа Weather</param>
    public void Update(Weather weather)
    {
        _dbContext.Entry(weather).State = EntityState.Modified;
        _dbContext.SaveChanges();
    }

    /// <summary>
    /// Метод удаление объекта типа Weather, а также изменение в таблицы Weathers из базы данных
    /// </summary>
    /// <param name="id">id объекта типа Weather</param>
    public void Delete(int id)
    {
        Weather? weather = FindById(id);
        if (weather is not null)
        {
            _dbContext.Weathers.Remove(weather);
            _dbContext.SaveChanges();
        }
    }

    /// <summary>
    /// Виртуальный метод, для удаление контекст базы данных
    /// </summary>
    /// <param name="disposing">Для вызова в контексте базы данных метод,Dispose в интерфейсе IDisposable</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!this._disposed)
        {
            if (disposing)
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