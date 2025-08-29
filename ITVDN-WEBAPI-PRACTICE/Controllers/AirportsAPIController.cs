using ClassLibrary.Data.Context;
using ClassLibrary.Data.Models;
using ClassLibrary.Data.Repositories;
using ClassLibrary.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ITVDN_WEBAPI_PRACTICE.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AirportsAPIController : ControllerBase
{
    #region FIELDS
    private IRepository<Airport> _repository;
    private readonly ILogger<AirportsAPIController> _logger;
    #endregion

    #region CONSTRUCTOR
    public AirportsAPIController(ILogger<AirportsAPIController> logger)
    {
        _repository = new AirportRepository(new WebServiceContext());
        this._logger = logger;
    }
    #endregion


    #region METHODS
    /// <summary>
    /// Метод, получение коллекцию класса Airport в асинхронном запросе (GET)
    /// </summary>
    /// <returns>Возвращает асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task, 
    /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
    /// обобщенную коллекцию типа IEnumerable, внутри которого тип класса Airport</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Airport>>> GetAllAirportsAsync()
        => await Task.Run(() => Ok(_repository.GetAll()));


    /// <summary>
    /// Метод, получение объекта класса Airport, через id в асинхронном запросе (GET)
    /// </summary>
    /// <param name="id">id объекта класса Airport</param>
    /// <returns>Возвращает асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task,
    /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
    /// объект класса Airport
    /// </returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Airport>> GetByAirportByIdAsync(int id)
        => await Task.Run(() => Ok(_repository.GetById(id)));


    /// <summary>
    /// Метод, добавление объекта класса Airport, в таблицу Airports, в асинхронном запросе (POST)
    /// </summary>
    /// <param name="airport">Объект класса Airport</param>
    /// <returns>Возвращает созданный объект асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task,
    /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
    /// объект класса Airport
    /// </returns>
    [HttpPost]
    public async Task<ActionResult<Airport>> CreateAirportAsync([FromQuery] Airport airport)
    {
        if(airport.Information == null)
        {
            _logger.LogError("Information is null");
            return await Task.Run(() => BadRequest());
        }
        else if(airport.CodeAirports == null)
        {
            _logger.LogError("CodeAirports is null");
            return await Task.Run(() => BadRequest());
        }
        else if(airport.PitStops == null)
        {
            _logger.LogError("PitStops is null");
            return await Task.Run(() => BadRequest());
        }
        else if(airport.DepartureAndArrivals == null)
        {
            _logger.LogError("DepartureAndArrivals is null");
            return await Task.Run(() => BadRequest());
        }
        else if(airport == null)
        {
            _logger.LogError("Airport is null");
            return await Task.Run(() => BadRequest());
        }

        _repository.Create(airport);
        return await Task.Run(() => Ok(airport));
    }



    /// <summary>
    /// Метод, изменение объекта класса Airport из таблицы Airports, в асинхронном запросе (PUT)
    /// </summary>
    /// <param name="airport">Объект класса Airport</param>
    /// <returns>Возвращает асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task,
    /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
    /// объект класса Airport
    /// </returns>
    [HttpPut]
    public async Task<ActionResult<Airport>> UpdateAirportAsync([FromQuery] Airport airport)
    {
        if (airport.Information == null)
        {
            _logger.LogError("Information is null");
            return await Task.Run(() => BadRequest());
        }
        else if (airport.CodeAirports == null)
        {
            _logger.LogError("CodeAirports is null");
            return await Task.Run(() => BadRequest());
        }
        else if (airport.PitStops == null)
        {
            _logger.LogError("PitStops is null");
            return await Task.Run(() => BadRequest());
        }
        else if (airport.DepartureAndArrivals == null)
        {
            _logger.LogError("DepartureAndArrivals is null");
            return await Task.Run(() => BadRequest());
        }
        else if (airport == null)
        {
            _logger.LogError("Airport is null");
            return await Task.Run(() => BadRequest());
        }

        _repository.Update(airport);
        return await Task.Run(() => Ok(airport));
    }


    /// <summary>
    /// Метод, удаление объекта класса Airport через id из таблицы Airports, в аснхронном запросе (DELETE)
    /// </summary>
    /// <param name="id">id объекта класса Airport</param>
    /// <returns>Возвращает асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task,
    /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
    /// объект класса Airport
    /// </returns>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Airport>> DeleteAirportAsync(int id)
    {
        Airport? airport = _repository.GetById(id);

        if (airport.Information == null)
        {
            _logger.LogError("Information is null");
            return await Task.Run(() => BadRequest());
        }
        else if (airport.CodeAirports == null)
        {
            _logger.LogError("CodeAirports is null");
            return await Task.Run(() => BadRequest());
        }
        else if (airport.PitStops == null)
        {
            _logger.LogError("PitStops is null");
            return await Task.Run(() => BadRequest());
        }
        else if (airport.DepartureAndArrivals == null)
        {
            _logger.LogError("DepartureAndArrivals is null");
            return await Task.Run(() => BadRequest());
        }
        else if (airport == null)
        {
            _logger.LogError("Airport is null");
            return await Task.Run(() => BadRequest());
        }

        _repository.Delete(id);

        return await Task.Run(() => Ok(airport));
    }
    #endregion
}