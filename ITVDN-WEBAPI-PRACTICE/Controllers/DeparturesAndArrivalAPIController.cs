using ClassLibrary.Data.Context;
using ClassLibrary.Data.Models;
using ClassLibrary.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ITVDN_WEBAPI_PRACTICE.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DeparturesAndArrivalAPIController : ControllerBase
{
    #region FIELDS
    private IRepository<DepartureAndArrival> _repository;
    private readonly ILogger<DeparturesAndArrivalAPIController> _logger;
    #endregion

    #region CONSTRUCTOR
    public DeparturesAndArrivalAPIController(ILogger<DeparturesAndArrivalAPIController> logger)
    {
        _repository = new DepartureAndArrivalRepository(new WebServiceContext());
        this._logger = logger;
    }
    #endregion


    #region METHODS

    /// <summary>
    /// Метод, получение коллекцию класса DepartureAndArrival в асинхронном запросе (GET)
    /// </summary>
    /// <returns>Возвращает асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task, 
    /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
    /// обобщенную коллекцию типа IEnumerable, внутри которого тип класса DepartureAndArrival</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DepartureAndArrival>>> GetAllDeparturesAndArrivalsAsync()
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
    public async Task<ActionResult<DepartureAndArrival>> GetDepartureAndArrivalById(int id)
        => await Task.Run(() => Ok(_repository.GetById(id)));



    /// <summary>
    /// Метод, добавление объекта класса DepartureAndArrival, в таблицу DeparturesAndArrivals, в асинхронном запросе (POST)
    /// </summary>
    /// <param name="depAndArr">Объект класса DepartureAndArrival</param>
    /// <returns>Возвращает созданный объект асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task,
    /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
    /// объект класса DepartureAndArrival
    /// </returns>
    [HttpPost]
    public async Task<ActionResult<DepartureAndArrival>> CreateDepartureAndArrivalAsync([FromQuery] DepartureAndArrival depAndArr)
    {
        if(depAndArr.Airport == null)
        {
            _logger.LogError("Airport is null");
            return await Task.Run(() => BadRequest());
        }
        else if(depAndArr.PitStops == null)
        {
            _logger.LogError("PitStops is null");
            return await Task.Run(() => BadRequest());
        }
        else if(depAndArr == null)
        {
            _logger.LogError("DepartureAndArrival is null");
            return await Task.Run(() => BadRequest());
        }

        _repository.Create(depAndArr);

        return await Task.Run(() => Ok(depAndArr));
    }


    /// <summary>
    /// Метод, изменение объекта класса DepartureAndArrival из таблицы DeparturesAndArrivals, в асинхронном запросе (PUT)
    /// </summary>
    /// <param name="depAndArr">Объект класса DepartureAndArrival</param>
    /// <returns>Возвращает асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task,
    /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
    /// объект класса DepartureAndArrival
    /// </returns>
    [HttpPut]
    public async Task<ActionResult<DepartureAndArrival>> UpdateDepartureAndArrivalAsync([FromQuery] DepartureAndArrival depAndArr)
    {
        if (depAndArr.Airport == null)
        {
            _logger.LogError("Airport is null");
            return await Task.Run(() => BadRequest());
        }
        else if (depAndArr.PitStops == null)
        {
            _logger.LogError("PitStops is null");
            return await Task.Run(() => BadRequest());
        }
        else if (depAndArr == null)
        {
            _logger.LogError("DepartureAndArrival is null");
            return await Task.Run(() => BadRequest());
        }

        _repository.Update(depAndArr);

        return await Task.Run(() => Ok(depAndArr));
    }


    /// <summary>
    /// Метод, удаление объекта класса DepartureAndArrival через id из таблицы DeparturesAndArrivals, в аснхронном запросе (DELETE)
    /// </summary>
    /// <param name="id">id объекта класса DepartureAndArrival</param>
    /// <returns>Возвращает асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task,
    /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
    /// объект класса DepartureAndArrival
    /// </returns>
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<DepartureAndArrival>> DeleteDepartureAndArrivalAsync(int id)
    {
        DepartureAndArrival? depAndArr = _repository.GetById(id);

        if (depAndArr.Airport == null)
        {
            _logger.LogError("Airport is null");
            return await Task.Run(() => BadRequest());
        }
        else if (depAndArr.PitStops == null)
        {
            _logger.LogError("PitStops is null");
            return await Task.Run(() => BadRequest());
        }
        else if (depAndArr == null)
        {
            _logger.LogError("DepartureAndArrival is null");
            return await Task.Run(() => BadRequest());
        }

        _repository.Delete(id);

        return await Task.Run(() => Ok(depAndArr));
    }
    #endregion
}