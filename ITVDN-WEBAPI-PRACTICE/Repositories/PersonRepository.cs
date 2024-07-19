using ITVDN_WEBAPI_PRACTICE.Context;
using ITVDN_WEBAPI_PRACTICE.Models;
using Microsoft.EntityFrameworkCore;
namespace ITVDN_WEBAPI_PRACTICE.Repositories;

public class PersonRepository : IRepository<Person>
{
    #region FIELDS
    private PersonContext db;
    private bool disposed = false;
    #endregion
    #region CONSTRUCTOR
    public PersonRepository(PersonContext db) => this.db = db;
    #endregion
    #region METHODS

    /// <summary>
    /// Метод, для возвращение коллекцию из базы данных
    /// </summary>
    /// <returns>Возвращающий тип коллекция типа Person</returns>
    public IEnumerable<Person> GetPeople() => db.People;

    /// <summary>
    /// Получение одного объекта класса Person через id
    /// </summary>
    /// <param name="id">id объекта класса Person</param>
    /// <returns>Возвращает объект класса Person</returns>
    public Person GetPerson(int id)
    {
        var person = db.People.FirstOrDefault(p => p.Id == id);

        if (person == null)
            return null!;
        return person;
    }


    /// <summary>
    /// Добавление объекта класса Person в таблицу People из базы данных
    /// </summary>
    /// <param name="person">объект класса Person</param>
    public void CreatePerson(Person person)
    {
        db.People.Add(person);
        db.SaveChanges();
    }

    /// <summary>
    /// Изменение объекта класса Person из таблицы People
    /// </summary>
    /// <param name="person">объекта класса Person</param>
    public void UpdatePerson(Person person)
    {
        db.Entry(person).State = EntityState.Modified;
        db.SaveChanges();
    }

    /// <summary>
    /// Удаление объекта класса Person из таблицы People
    /// </summary>
    /// <param name="id">id объекта класса Person</param>
    public void DeletePerson(int id)
    {
        Person? person = db.People.FirstOrDefault(x => x.Id == id);
        if (person != null)
        {
            db.People.Remove(person);
            db.SaveChanges();
        }

    }

    /// <summary>
    /// Виртуальный метод, для удаление контекст базы данных
    /// </summary>
    /// <param name="disposing">для вызова в контексте базы данных метод, Dispose в интерфейсе IDisposable</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
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
