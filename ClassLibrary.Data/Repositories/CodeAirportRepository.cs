using ClassLibrary.Data.Context;
using ClassLibrary.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary.Data.Repositories;

public class CodeAirportRepository : IRepository<CodeAirport>
{
    #region FIELDS
    private WebServiceContext _dbContext;
    private bool _disposed = false;
    #endregion

    #region CONSTRUCTOR
    public CodeAirportRepository(WebServiceContext dbContext) => this._dbContext = dbContext;
    #endregion


    #region METHODS

    /// <summary>
    /// Метод получение коллекция объекта типа CodeAirport
    /// </summary>
    /// <returns>Возвращает коллекция объекта типа CodeAirport</returns>
    public IEnumerable<CodeAirport> GetAll()
    {
        return _dbContext.CodeAirports
            .Include(x => x.Airport);
    }

    /// <summary>
    /// Метод получение объекта типа CodeAirport по id
    /// </summary>
    /// <param name="id">id объекта типа CodeAirport</param>
    /// <returns>Возвращает объект типа CodeAirport</returns>
    public CodeAirport GetById(int id)
    {
        CodeAirport? codeAirport = _dbContext.CodeAirports.
            Include(x => x.Airport).FirstOrDefault(x => x.Id == id);

        if(codeAirport == null)
        {
            return null!;
        }
        return codeAirport;
    }


    /// <summary>
    /// Создание объекта типа CodeAirport, а также добавление в таблицу CodeAirports из базы данных
    /// </summary>
    /// <param name="codeAirport">Объект типа CodeAirport</param>
    public void Create(CodeAirport codeAirport)
    {
        _dbContext.CodeAirports .Add(codeAirport);
        _dbContext.SaveChanges();
    }


    /// <summary>
    /// Метод, изменние объъекта типа CodeAirport, а также сохранение в таблицу CodeAirports из базы данных
    /// </summary>
    /// <param name="codeAirport">Объект типа CodeAirport</param>
    public void Update(CodeAirport codeAirport)
    {
        _dbContext.Entry(codeAirport).State = EntityState.Modified;
        _dbContext.SaveChanges();
    }

    /// <summary>
    /// Метод удаление объекта типа CodeAirport, а также изменение в таблицы CodeAirports из базы данных
    /// </summary>
    /// <param name="id">id объекта типа CodeAirport</param>
    public void Delete(int id)
    {
        CodeAirport? codeAirport = GetById(id);
        if (codeAirport != null)
        {
            _dbContext.CodeAirports.Remove(codeAirport);
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