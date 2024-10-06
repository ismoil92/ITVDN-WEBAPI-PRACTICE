using ITVDN_WEBAPI_PRACTICE.Context;
using ITVDN_WEBAPI_PRACTICE.Models;
using ITVDN_WEBAPI_PRACTICE.Repositories;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ITVDN_WEBAPI_PRACTICE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        #region FIELDS
        private IRepository<Car> repository;
        private readonly ILogger<CarsController> logger;
        #endregion

        #region CONSTRUCTOR
        public CarsController(ILogger<CarsController> logger)
        {
            repository = new CarRepository(new CarContext());
            this.logger = logger;
        }
        #endregion

        #region FIELDS


        /// <summary>
        /// Метод, получение коллекцию класса Car в асинхронном запросе (GET)
        /// </summary>
        /// <returns>Возвращает асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task, 
        /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
        /// обобщенную коллекцию типа IEnumerable, внутри которого тип класса Class</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCarsAsync() =>
            await Task.Run(() => Ok(repository.GetAllCars()));


        /// <summary>
        /// Метод, получение объекта класса Car, через id в асинхронном запросе (GET)
        /// </summary>
        /// <param name="id">id объекта класса Car</param>
        /// <returns>Возвращает асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task,
        /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
        /// объект класса Car
        /// </returns>

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Car>> GetCarAsync(int id) =>
            await Task.Run(() => Ok(repository.GetCar(id)));



        /// <summary>
        /// Метод, добавление объекта класса Car, в таблицу Cars, в асинхронном запросе (POST)
        /// </summary>
        /// <param name="car">Объект класса Car</param>
        /// <returns>Возвращает созданный объект асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task,
        /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
        /// объект класса Car
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<Car>> CreateCarAsync([FromQuery] Car car)
        {
            if (car == null)
            {
                logger.LogError("Incorrect data type Car");
                return await Task.Run(() => BadRequest());
            }

            repository.CreateCar(car!);
            return await Task.Run(() => Ok(car));
        }




        /// <summary>
        /// Метод, изменение объекта класса Car из таблицы Cars, в асинхронном запросе (PUT)
        /// </summary>
        /// <param name="car">Объект класса car</param>
        /// <returns>Возвращает асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task,
        /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
        /// объект класса Car
        /// </returns>
        [HttpPut]
        public async Task<ActionResult<Car>> UpdateCarAsync([FromQuery] Car car)
        {
            if(car == null)
            {
                logger.LogError("Incorrect data type Car");
                return await Task.Run(() => BadRequest());
            }

            repository.UpdateCar(car);
            return await Task.Run(() => Ok(car));
        }


        /// <summary>
        /// Метод, удаление объекта класса Car через id из таблицы Cars, в аснхронном запросе (DELETE)
        /// </summary>
        /// <param name="id">id объекта класса Car</param>
        /// <returns>Возвращает асинхронный обобщенный тип класса Task. Внутри обобщенного класса Task,
        /// имеется другой обобщенный класс ActionResult. А внутри обобщенного класса ActionResult, возвращает
        /// объект класса Car
        /// </returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Car>> RemoveCarAsync(int id)
        {
            var car = repository.GetCar(id);
            if (car == null)
            {
                logger.LogError("Incorrect data type Car");
                return await Task.Run(() => BadRequest());
            }

            repository.DeleteCar(id);
            return await Task.Run(() => Ok(car));
        }


        /// <summary>
        /// Метод, для обработки ошибок во время запроса WEB API
        /// </summary>
        /// <param name="env">объект интерфейса IHostEnvironment. Этот интерфейс
        /// предсталяет сведения о среде размещения, в которой выполняется приложение</param>
        /// <returns>Возвращает объект интерфейса IActionResult, информацию об ошибке</returns>
        [Route("/error-development")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult HandleErrorDevelopment([FromServices] IHostEnvironment env)
        {
            if(!env.IsDevelopment())
            {
                return NotFound();
            }

            var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>()!;
            logger.LogError($"\n\nError Detail:{exceptionHandlerFeature.Error.StackTrace}\n\nTitle:{exceptionHandlerFeature.Error.Message}");

            return Problem(detail: exceptionHandlerFeature.Error.StackTrace,
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
}
