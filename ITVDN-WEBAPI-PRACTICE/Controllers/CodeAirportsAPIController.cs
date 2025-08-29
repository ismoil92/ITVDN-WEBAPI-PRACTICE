using ClassLibrary.Data.Context;
using ClassLibrary.Data.Models;
using ClassLibrary.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ITVDN_WEBAPI_PRACTICE.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CodeAirportsAPIController : ControllerBase
{
    #region FIELDS
    private IRepository<CodeAirport> _repository;
    private readonly ILogger<CodeAirportsAPIController> _logger;
    #endregion


    #region CONSTRUCTOR
    public CodeAirportsAPIController(ILogger<CodeAirportsAPIController> logger)
    {
        _repository = new CodeAirportRepository(new WebServiceContext());
        _logger = logger;
    }
    #endregion

    #region METHODS

    /// <summary>
    /// Метод, получение коллекцию класса CodeAirport в асинхронном запросе (GET)
    /// </summary>
    /// <returns>Возвращает асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task, 
    /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
    /// обобщенную коллекцию типа IEnumerable, внутри которого тип класса CodeAirport</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CodeAirport>>> GetAllCodeAirportsAsync()
        => await Task.Run(() => Ok(_repository.GetAll()));



    /// <summary>
    /// Метод, получение объекта класса CodeAirport, через id в асинхронном запросе (GET)
    /// </summary>
    /// <param name="id">id объекта класса CodeAirport</param>
    /// <returns>Возвращает асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task,
    /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
    /// объект класса CodeAirport
    /// </returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CodeAirport>> GetCodeAirportById(int id)
        => await Task.Run(() => Ok(_repository.GetById(id)));




    /// <summary>
    /// Метод, добавление объекта класса CodeAirport, в таблицу CodeAirports, в асинхронном запросе (POST)
    /// </summary>
    /// <param name="codeAirport">Объект класса CodeAirport</param>
    /// <returns>Возвращает созданный объект асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task,
    /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
    /// объект класса CodeAirport
    /// </returns>
    [HttpPost]
    public async Task<ActionResult<CodeAirport>> CreateCodeAirportAsync([FromQuery] CodeAirport codeAirport)
    {

        if(codeAirport.Airport == null)
        {
            _logger.LogError("Airport is null");
            return await Task.Run(() => BadRequest());
        }
        else if(codeAirport == null)
        {
            _logger.LogError("CodeAirport is null");
            return await Task.Run(() => BadRequest());
        }

        _repository.Create(codeAirport);

        return await Task.Run(() => Ok(codeAirport));
    }

    /// <summary>
    /// Метод, изменение объекта класса CodeAirport из таблицы CodeAirports, в асинхронном запросе (PUT)
    /// </summary>
    /// <param name="codeAirport">Объект класса CodeAirport</param>
    /// <returns>Возвращает асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task,
    /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
    /// объект класса CodeAirport
    /// </returns>
    [HttpPut]
   public async Task<ActionResult<CodeAirport>> UpdateCodeAirportAsync([FromQuery] CodeAirport codeAirport)
    {
        if (codeAirport.Airport == null)
        {
            _logger.LogError("Airport is null");
            return await Task.Run(() => BadRequest());
        }
        else if (codeAirport == null)
        {
            _logger.LogError("CodeAirport is null");
            return await Task.Run(() => BadRequest());
        }

        _repository.Update(codeAirport);

        return await Task.Run(() => Ok(codeAirport));
    }


    /// <summary>
    /// Метод, удаление объекта класса CodeAirport через id из таблицы CodeAirports, в аснхронном запросе (DELETE)
    /// </summary>
    /// <param name="id">id объекта класса CodeAirport</param>
    /// <returns>Возвращает асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task,
    /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
    /// объект класса CodeAirport
    /// </returns>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<CodeAirport>> DeleteCodeAirportAsync(int id)
    {
        CodeAirport? codeAirport = _repository.GetById(id);

        if (codeAirport.Airport == null)
        {
            _logger.LogError("Airport is null");
            return await Task.Run(() => BadRequest());
        }
        else if (codeAirport == null)
        {
            _logger.LogError("CodeAirport is null");
            return await Task.Run(() => BadRequest());
        }

        _repository.Delete(id);

        return await Task.Run(() => Ok(codeAirport));
    }

    #endregion

}