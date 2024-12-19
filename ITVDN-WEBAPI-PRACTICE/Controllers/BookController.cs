using ITVDN_WEBAPI_PRACTICE.Context;
using ITVDN_WEBAPI_PRACTICE.Models;
using ITVDN_WEBAPI_PRACTICE.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ITVDN_WEBAPI_PRACTICE.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    #region FIELDS
    private IRepository<Book> _repo;
    private readonly ILogger<BookController> logger;
    #endregion
    #region CONSTUCTOR
    public BookController(ILogger<BookController> logger)
    {
        _repo = new BookRepository(new BookContext());
        this.logger = logger;
    }
    #endregion


    #region METHODS


    /// <summary>
    /// Метод, получение коллекцию класса Book в асинхронном запросе (GET)
    /// </summary>
    /// <returns>Возвращает асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task, 
    /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
    /// обобщенную коллекцию типа IEnumerable, внутри которого тип класса Book</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetAllCarsAsync() =>
        await Task.Run(() => Ok(_repo.GetAllBooks()));


    /// <summary>
    /// Метод, получение объекта класса Book, через id в асинхронном запросе (GET)
    /// </summary>
    /// <param name="id">id объекта класса Book</param>
    /// <returns>Возвращает асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task,
    /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
    /// объект класса Book
    /// </returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Book>> GetBookById(int id) =>
        await Task.Run(() => Ok(_repo?.GetBookById(id)));


    /// <summary>
    /// Метод, добавление объекта класса Book, в таблицу Books, в асинхронном запросе (POST)
    /// </summary>
    /// <param name="book">Объект класса Book</param>
    /// <returns>Возвращает созданный объект асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task,
    /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
    /// объект класса Book
    /// </returns>
    [HttpPost]
    public async Task<ActionResult<Book>> CreateBookAsync([FromQuery] Book book)
    {
        if (book == null)
        {
            logger.LogError("Incorrect data type Book");
            return await Task.Run(() => BadRequest());
        }

        _repo.CreateBook(book);
        return await Task.Run(() => Ok(book));
    }


    /// <summary>
    /// Метод, изменение объекта класса Book из таблицы Books, в асинхронном запросе (PUT)
    /// </summary>
    /// <param name="book">Объект класса Book</param>
    /// <returns>Возвращает асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task,
    /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
    /// объект класса Book
    /// </returns>
    [HttpPut]
    public async Task<ActionResult<Book>> UpdateBookAsync([FromQuery] Book book)
    {
        if (book == null)
        {
            logger.LogError("Incorrect date type Book");
            return await Task.Run(() => BadRequest());
        }

        _repo.UpdateBook(book);
        return await Task.Run(() => Ok(book));
    }

    /// <summary>
    /// Метод, удаление объекта класса Book через id из таблицы Books, в аснхронном запросе (DELETE)
    /// </summary>
    /// <param name="id">id объекта класса Book</param>
    /// <returns>Возвращает асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task,
    /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
    /// объект класса Book
    /// </returns>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Book>> DeleteBookAsync(int id)
    {
        var book = _repo.GetBookById(id);
        if(book == null)
        {
            logger.LogError("Incorrect data type Book");
            return await Task.Run(() => BadRequest());
        }

        _repo.DeleteBook(id);
        return await Task.Run(() => Ok(book));
    }

    #endregion
}
