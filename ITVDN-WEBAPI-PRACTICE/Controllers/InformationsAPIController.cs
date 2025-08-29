using ClassLibrary.Data.Context;
using ClassLibrary.Data.Models;
using ClassLibrary.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ITVDN_WEBAPI_PRACTICE.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InformationsAPIController : ControllerBase
{
    #region FIELDS
    private IRepository<Information> _repository;
    private readonly ILogger<InformationsAPIController> _logger;
    #endregion


    #region CONSTRUCTOR
    public InformationsAPIController(ILogger<InformationsAPIController> logger)
    {
        _repository = new InformationRepository(new WebServiceContext());
        _logger = logger;
    }
    #endregion

    /// <summary>
    /// Метод, получение коллекцию класса Information в асинхронном запросе (GET)
    /// </summary>
    /// <returns>Возвращает асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task, 
    /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
    /// обобщенную коллекцию типа IEnumerable, внутри которого тип класса Information</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Information>>> GetAllInformationsAsync()
        => await Task.Run(() => Ok(_repository.GetAll()));


    /// <summary>
    /// Метод, получение объекта класса Information, через id в асинхронном запросе (GET)
    /// </summary>
    /// <param name="id">id объекта класса Information</param>
    /// <returns>Возвращает асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task,
    /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
    /// объект класса Information
    /// </returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Information>> GetInformationById(int id)
        => await Task.Run(() => Ok(_repository.GetById(id)));


    /// <summary>
    /// Метод, добавление объекта класса Information, в таблицу Informations, в асинхронном запросе (POST)
    /// </summary>
    /// <param name="information">Объект класса Information</param>
    /// <returns>Возвращает созданный объект асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task,
    /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
    /// объект класса Information
    /// </returns>
    [HttpPost]
    public async Task<ActionResult<Information>> CreateInformationAsync([FromQuery] Information information)
    {
        if(information.Airport == null)
        {
            _logger.LogError("Airport is null");
            return await Task.Run(() => BadRequest());
        }
        else if(information == null)
        {
            _logger.LogError("Information is null");
            return await Task.Run(() => BadRequest());
        }

        _repository.Create(information);

        return await Task.Run(() => Ok(information));
    }


    /// <summary>
    /// Метод, изменение объекта класса Information из таблицы Informations, в асинхронном запросе (PUT)
    /// </summary>
    /// <param name="information">Объект класса Information</param>
    /// <returns>Возвращает асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task,
    /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
    /// объект класса Information
    /// </returns>
    [HttpPut]
    public async Task<ActionResult<Information>> UpdateInformationAsync([FromQuery] Information information)
    {
        if (information.Airport == null)
        {
            _logger.LogError("Airport is null");
            return await Task.Run(() => BadRequest());
        }
        else if (information == null)
        {
            _logger.LogError("Information is null");
            return await Task.Run(() => BadRequest());
        }

        _repository.Update(information);
        return await Task.Run(() => Ok(information));
    }


    /// <summary>
    /// Метод, удаление объекта класса Information через id из таблицы Informations, в аснхронном запросе (DELETE)
    /// </summary>
    /// <param name="id">id объекта класса Information</param>
    /// <returns>Возвращает асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task,
    /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
    /// объект класса Information
    /// </returns>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Information>> DeleteInformationAsync(int id)
    {
        Information? information = _repository.GetById(id);

        if (information.Airport == null)
        {
            _logger.LogError("Airport is null");
            return await Task.Run(() => BadRequest());
        }
        else if (information == null)
        {
            _logger.LogError("Information is null");
            return await Task.Run(() => BadRequest());
        }

        _repository.Delete(id);
        return await Task.Run(() => Ok(information));
    }
}