using ClassLibrary.Data.Context;
using ClassLibrary.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace ClassLibrary.Data.Repositories;

public class InformationRepository : IRepository<Information>
{
    #region FIELDS
    private WebServiceContext _dbContext;
    private bool _disposed = false;
    #endregion


    #region CONSTRUCTOR
    public InformationRepository(WebServiceContext dbContext) => this._dbContext = dbContext;
    #endregion


    #region METHODS

    /// <summary>
    /// Метод получение коллекция объекта типа Information
    /// </summary>
    /// <returns>Возвращает коллекция объекта типа Information</returns>
    public IEnumerable<Information> GetAll()
    {
        return _dbContext.Informations.Include(x => x.Airport);
    }


    /// <summary>
    /// Метод получение объекта типа Information по id
    /// </summary>
    /// <param name="id">id объекта типа Information</param>
    /// <returns>Возвращает объект типа Information</returns>
    public Information GetById(int id)
    {
        Information? information = _dbContext.Informations.
            Include(x => x.Airport).FirstOrDefault(x => x.Id == id);

        if(information == null)
        {
            return null!;
        }
        return information;
    }


    /// <summary>
    /// Создание объекта типа Information, а также добавление в таблицу Informations из базы данных
    /// </summary>
    /// <param name="information">Объект типа Information</param>
    public void Create(Information information)
    {
        _dbContext.Informations.Add(information);
        _dbContext.SaveChanges();
    }

    /// <summary>
    /// Метод, изменние объъекта типа Information, а также сохранение в таблицу Informations из базы данных
    /// </summary>
    /// <param name="information">Объект типа Information</param>
    public void Update(Information information)
    {
        _dbContext.Entry(information).State = EntityState.Modified;
        _dbContext.SaveChanges();
    }

    /// <summary>
    /// Метод удаление объекта типа Information, а также изменение в таблицы Informations из базы данных
    /// </summary>
    /// <param name="id">id объекта типа Information</param>
    public void Delete(int id)
    {
        Information? information = GetById(id);
        if(information != null)
        {
            _dbContext.Informations.Remove(information);
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