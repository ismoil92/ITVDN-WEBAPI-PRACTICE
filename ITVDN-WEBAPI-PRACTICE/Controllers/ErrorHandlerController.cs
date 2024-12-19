using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ITVDN_WEBAPI_PRACTICE.Controllers
{
    [Route("ErrorHandler")]
    [ApiController]
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorHandlerController : ControllerBase
    {

        #region FIELD
        private readonly ILogger<ErrorHandlerController> logger;
        #endregion

        #region CONSTUCTOR
        public ErrorHandlerController(ILogger<ErrorHandlerController> logger)
        {
            this.logger = logger;
        }
        #endregion


        #region METHODS

        /// <summary>
        /// Метод для обработки ошибок во время запроса WEB API
        /// </summary>
        /// <param name="environment">Объект интерфейса IHostEnvironment.
        /// Этот интерфейс представляет сведения о среде размещения, в которой выполняется приложение</param>
        /// <returns>Возвращает объект интерфейса IActionResult, информацию об ошибке</returns>
        [Route("ErrorDevelopment")]
        public IActionResult ErrorDevelopment([FromServices] IHostEnvironment environment)
        {
            if (!environment.IsDevelopment())
            {
                return NotFound();
            }

            var exceptionHandlerFuture = HttpContext.Features.Get<IExceptionHandlerFeature>();

            //logger.LogError($"\n\nError Detail:{exceptionHandlerFuture!.Error.StackTrace}\n\nTitle:{exceptionHandlerFuture.Error.Message}");

            return Problem(detail: exceptionHandlerFuture!.Error.StackTrace,
                title: exceptionHandlerFuture.Error.Message);
        }


        /// <summary>
        /// Метод обработки ошибок при запуске приложение
        /// </summary>
        /// <returns>Объект интерфейса IActionResult, информацию об ошибке</returns>
        [Route("Error")]
        public IActionResult Error() => Problem();
        #endregion
    }
}
