using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ITVDN_WEBAPI_PRACTICE.Controllers;

[Route("ErrorHandler")]
[ApiController]
[AllowAnonymous]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorHandlerController : ControllerBase
{

    #region METHODS
    /// <summary>
    /// Метод для обработки ошибок во время запроса WEB API
    /// </summary>
    /// <param name="hostEnvironment">Объект интерфейса IHostEnvironment.
    /// Этот интерфейс представляет сведения о среде размещения, в которой выполняется приложение</param>
    /// <returns>Возвращает объект интерфейса IActionResult, информацию об ошибке</returns>
    [Route("ErrorDevelopment")]
    public IActionResult ErrorDevelopment([FromServices] IHostEnvironment hostEnvironment)
    {
        if(!hostEnvironment.IsDevelopment())
        {
            return NotFound();
        }

        var _exceptionHandlerFuture = HttpContext.Features.Get<IExceptionHandlerFeature>();

        return Problem(detail: _exceptionHandlerFuture!.Error.StackTrace,
            title: _exceptionHandlerFuture!.Error.Message);
    }


    /// <summary>
    /// Метод обработки ошибок при запуске приложение
    /// </summary>
    /// <returns>Объект интерфейса IActionResult, информацию об ошибке</returns>
    [Route("Error")]
    public IActionResult Error() => Problem();
    #endregion
}