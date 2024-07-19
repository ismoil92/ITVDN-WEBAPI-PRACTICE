using ITVDN_WEBAPI_PRACTICE.Context;
using ITVDN_WEBAPI_PRACTICE.Models;
using ITVDN_WEBAPI_PRACTICE.Repositories;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ITVDN_WEBAPI_PRACTICE.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonController : ControllerBase
{
    #region FIELDS
    private IRepository<Person> repository;
    private readonly ILogger<PersonController> logger;
    #endregion
    #region CONTRUCTOR
    public PersonController(ILogger<PersonController> logger)
    {
        repository = new PersonRepository(new PersonContext());
        this.logger = logger;
    }

    #endregion
    #region METHODS

    /// <summary>
    /// Метод, получение коллекцию класса Person в асинхронном запросе (GET)
    /// </summary>
    /// <returns>Возвращает асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task, 
    /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
    /// обобщенную коллекцию типа IEnumerable, внутри которого тип класса Person</returns>

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Person>>> GetPeopleAsync() =>
        await Task.Run(() => Ok(repository.GetPeople()));

    /// <summary>
    /// Метод, получение объекта класса Person, через id в асинхронном запросе (GET)
    /// </summary>
    /// <param name="id">id объекта класса Person</param>
    /// <returns>Возвращает асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task,
    /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
    /// объект класса Person
    /// </returns>

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Person>> GetPersonAsync(int id) =>
        await Task.Run(() => Ok(repository.GetPerson(id)));


    /// <summary>
    /// Метод, добавление объекта класса Person, в таблицу People, в асинхронном запросе (POST)
    /// </summary>
    /// <param name="person">Объект класса Person</param>
    /// <returns>Возвращает созданный объект асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task,
    /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
    /// объект класса Person
    /// </returns>
    [HttpPost]
    public async Task<ActionResult<Person>> CreatePersonAsync([FromQuery] Person person)
    {
        if(person == null)
        {
            logger.LogError("Incorrect data person");
            return await Task.Run(() => BadRequest());
        }

        repository.CreatePerson(person);
        return await Task.Run(() => Ok(person));
    }


    /// <summary>
    /// Метод, изменение объекта класса Person из таблицы People, в асинхронном запросе (PUT)
    /// </summary>
    /// <param name="person">Объект класса person</param>
    /// <returns>Возвращает асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task,
    /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
    /// объект класса Person
    /// </returns>
    [HttpPut]
    public async Task<ActionResult<Person>> UpdatePersonAsync([FromQuery] Person person)
    {
        if(person == null)
        {
            logger.LogError("Incorrect data person");
            return await Task.Run(() => BadRequest());
        }

        repository.UpdatePerson(person);

        return await Task.Run(() => Ok(person));
    }

    /// <summary>
    /// Метод, удаление объекта класса Person через id из таблицы People, в аснхронном запросе (DELETE)
    /// </summary>
    /// <param name="id">id объекта класса Person</param>
    /// <returns>Возвращает асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task,
    /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
    /// объект класса Person
    /// </returns>

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Person>> RemovePersonAsync(int id)
    {
        var person = repository.GetPerson(id);

        if(person != null)
        {
            repository.DeletePerson(id);
            await Task.Run(() => Ok(person));
        }

        logger.LogError("Incorrect data person");
        return await Task.Run(() => BadRequest());
    }

    /// <summary>
    /// Метод, для обработки ошибок во время запроса WEB API
    /// </summary>
    /// <param name="hostEnvironment">объект интерфейса IHostEnvironment. Этот интерфейс
    /// предсталяет сведения о среде размещения, в которой выполняется приложение</param>
    /// <returns>Возвращает объект интерфейса IActionResult, информацию об ошибке</returns>

    [Route("/error-development")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult HandleErrorDevelopment([FromServices] IHostEnvironment hostEnvironment)
    {
        if(!hostEnvironment.IsDevelopment())
        {
            return NotFound();
        }

        var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>()!;

        logger.LogError($"\n\nError Detail:{exceptionHandlerFeature.Error.StackTrace}, \n\n Title:{exceptionHandlerFeature.Error.Message}");

        return Problem(
            detail: exceptionHandlerFeature.Error.StackTrace,
            title: exceptionHandlerFeature.Error.Message);
    }

    /// <summary>
    /// Метод, для обработки ошибок при запуске приложение
    /// </summary>
    /// <returns>Возвращает объект интерфейса IActionResult, информацию об ошибке</returns>
    [Route("/error")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult HandleError() => Problem();
    #endregion
}