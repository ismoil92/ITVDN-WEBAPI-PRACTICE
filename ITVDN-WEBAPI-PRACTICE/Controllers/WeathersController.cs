using ClassLibrary.Data.Context;
using ClassLibrary.Data.Models;
using ClassLibrary.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ITVDN_WEBAPI_PRACTICE.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WeathersController : ControllerBase
{
    #region FIELDS
    public IRepository<Weather> _repository;
    public readonly ILogger<WeathersController> _logger;
    #endregion

    #region CONSTRUCTOR
    public WeathersController(ILogger<WeathersController> logger)
    {
        _repository = new WeatherRepository(new WeatherContext());
        this._logger = logger;
    }
    #endregion

    #region METHODS

    /// <summary>
    /// Метод, получение коллекцию класса Weather в асинхронном запросе (GET)
    /// </summary>
    /// <remarks>
    /// Запрос образца:
    /// 
    /// 
    ///       GET   /api/weathers
    /// 
    /// </remarks>
    /// 
    /// 
    /// <returns>Возвращает асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task, 
    /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
    /// обобщенную коллекцию типа IEnumerable, внутри которого тип класса Weather</returns>
    /// <response code="200">Запрос успешен</response>
    [HttpGet]

    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Weather>>> GetAllWeathersAsync()
        => await Task.Run(() => Ok(_repository.GetAll()));


    /// <summary>
    /// Метод, получение объекта класса Weather, через id в асинхронном запросе (GET)
    /// </summary>
    /// <remarks>
    /// Запрос образца:
    /// 
    /// 
    ///       GET   /api/weathers/{id}
    /// 
    /// </remarks>
    /// 
    /// 
    /// <param name="id">id объекта класса Weather</param>
    /// <returns>Возвращает асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task,
    /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
    /// объект класса Weather
    /// </returns>
    /// <response code="200">Запрос успешен</response>
    [HttpGet("{id:int}")]

    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Weather>> GetWeatherById(int id)
        => await Task.Run(() => Ok(_repository.FindById(id)));


    /// <summary>
    /// Метод, добавление объекта класса Weather, в таблицу Weathers, в асинхронном запросе (POST)
    /// </summary>
    ///<remarks>
    ///           Запрос образца:
    /// 
    /// 
    ///       Post   /api/weathers
    /// 
    /// </remarks>
    /// <param name="weather">Объект класса Weather</param>
    /// <returns>Возвращает созданный объект асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task,
    /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
    /// объект класса Weather
    /// </returns>
    /// <response code="200">Запрос успешен</response>
    /// <response code="400">Ошибка при запросе</response>
    [HttpPost]

    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Weather>> CreateWeatherAsync([FromQuery]  Weather weather)
    {
        if(weather is null)
        {
            _logger.LogError("Weather object is null");
            return await Task.Run(() => BadRequest());
        }

        _repository.Create(weather);
        return await Task.Run(() => Ok(weather));
    }

    /// <summary>
    /// Метод, изменение объекта класса Weather из таблицы Weathers, в асинхронном запросе (PUT)
    /// </summary>
    /// 
    /// ///<remarks>
    ///           Запрос образца:
    /// 
    /// 
    ///       Put   /api/weathers
    /// 
    /// </remarks>
    /// <param name="weather">Объект класса Weather</param>
    /// <returns>Возвращает асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task,
    /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
    /// объект класса Weather
    /// </returns>
    /// <response code="200">Запрос успешен</response>
    /// <response code="400">Ошибка при запросе</response>
    [HttpPut]

    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType (StatusCodes.Status200OK)]
    public async Task<ActionResult<Weather>> UpdateWeatherAsync([FromQuery] Weather weather)
    {
        if(weather is null)
        {
            _logger.LogError("Weather object is null");
            return await Task.Run(() => BadRequest());
        }

        _repository.Update(weather);
        return await Task.Run(() => Ok(weather));
    }


    /// <summary>
    /// Метод, удаление объекта класса Weather через id из таблицы Weathers, в аснхронном запросе (DELETE)
    /// </summary>
    /// 
    /// 
    /// ///<remarks>
    ///           Запрос образца:
    /// 
    /// 
    ///       Delete   /api/weathers/{id}
    /// 
    /// </remarks>
    /// <param name="id">id объекта класса Weather</param>
    /// <returns>Возвращает асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task,
    /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
    /// объект класса Weather
    /// </returns>
    /// <response code="200">Запрос успешен</response>
    /// <response code="400">Ошибка при запросе</response>
    [HttpDelete("{id:int}")]

    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Weather>> DeleteWeatherAsync(int id)
    {
        Weather? weather = _repository.FindById(id);

        if(weather is null)
        {
            _logger.LogError("Weather object is null");
            return await Task.Run(() => BadRequest());
        }

        _repository.Delete(id);
        return await Task.Run(() => Ok(weather));
    }
    #endregion
}