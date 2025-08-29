using ClassLibrary.Data.Context;
using ClassLibrary.Data.Models;
using ClassLibrary.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ITVDN_WEBAPI_PRACTICE.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PitStopsAPIController : ControllerBase
{
    #region FIELDS
    private IRepository<PitStop> _repository;
    private readonly ILogger<PitStopsAPIController> _logger;
    #endregion

    #region CONSTRUCTOR
    public PitStopsAPIController(ILogger<PitStopsAPIController> logger)
    {
        _repository = new PitStopRepository(new WebServiceContext());
        _logger = logger;
    }
    #endregion


    #region METHODS

    /// <summary>
    /// Метод, получение коллекцию класса PitStop в асинхронном запросе (GET)
    /// </summary>
    /// <returns>Возвращает асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task, 
    /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
    /// обобщенную коллекцию типа IEnumerable, внутри которого тип класса PitStop</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PitStop>>> GetAllPitStopsAsync()
        => await Task.Run(() => Ok(_repository.GetAll()));


    /// <summary>
    /// Метод, получение объекта класса PitStop, через id в асинхронном запросе (GET)
    /// </summary>
    /// <param name="id">id объекта класса PitStop</param>
    /// <returns>Возвращает асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task,
    /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
    /// объект класса PitStop
    /// </returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PitStop>> GetPitStopById(int id)
        => await Task.Run(() => _repository.GetById(id));


    /// <summary>
    /// Метод, добавление объекта класса PitStop, в таблицу PitStops, в асинхронном запросе (POST)
    /// </summary>
    /// <param name="pitStop">Объект класса Airport</param>
    /// <returns>Возвращает созданный объект асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task,
    /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
    /// объект класса PitStop
    /// </returns>
    [HttpPost]
    public async Task<ActionResult<PitStop>> CreatePitStopAsync([FromQuery]  PitStop pitStop)
    {
        if(pitStop.Airport == null)
        {
            _logger.LogError("Airport is null");
            return await Task.Run(() => BadRequest());
        }
        else if(pitStop.DepartureAndArrival == null)
        {
            _logger.LogError("DepartureAndArrival is null");
            return await Task.Run(() => BadRequest());
        }
        else if(pitStop == null)
        {
            _logger.LogError("PitStop is null");
            return await Task.Run(() => BadRequest());
        }

        _repository.Create(pitStop);

        return await Task.Run(() => Ok(pitStop));
    }


    /// <summary>
    /// Метод, изменение объекта класса PitStop из таблицы PitStops, в асинхронном запросе (PUT)
    /// </summary>
    /// <param name="pitStop">Объект класса Airport</param>
    /// <returns>Возвращает асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task,
    /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
    /// объект класса PitStop
    /// </returns>
    [HttpPut]
    public async Task<ActionResult<PitStop>> UpdatePitStopAsync([FromQuery] PitStop pitStop)
    {
        if (pitStop.Airport == null)
        {
            _logger.LogError("Airport is null");
            return await Task.Run(() => BadRequest());
        }
        else if (pitStop.DepartureAndArrival == null)
        {
            _logger.LogError("DepartureAndArrival is null");
            return await Task.Run(() => BadRequest());
        }
        else if (pitStop == null)
        {
            _logger.LogError("PitStop is null");
            return await Task.Run(() => BadRequest());
        }

        _repository.Update(pitStop);

        return await Task.Run(() => Ok(pitStop));
    }


    /// <summary>
    /// Метод, удаление объекта класса PitStop через id из таблицы PitStops, в аснхронном запросе (DELETE)
    /// </summary>
    /// <param name="id">id объекта класса PitStop</param>
    /// <returns>Возвращает асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task,
    /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
    /// объект класса PitStop
    /// </returns>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<PitStop>> DeletePitStopAsync(int id)
    {
        PitStop? pitStop = _repository.GetById(id);

        if (pitStop.Airport == null)
        {
            _logger.LogError("Airport is null");
            return await Task.Run(() => BadRequest());
        }
        else if (pitStop.DepartureAndArrival == null)
        {
            _logger.LogError("DepartureAndArrival is null");
            return await Task.Run(() => BadRequest());
        }
        else if (pitStop == null)
        {
            _logger.LogError("PitStop is null");
            return await Task.Run(() => BadRequest());
        }

        _repository.Delete(id);

        return await Task.Run(() => Ok(pitStop));
    }

    #endregion
}