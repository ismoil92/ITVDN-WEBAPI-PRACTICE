using ITVDN_WEBAPI_PRACTICE.Middlewares;
namespace ITVDN_WEBAPI_PRACTICE.Extensions;

public static class ErrorExceptionHandlerExtensions
{
    #region METHOD
    /// <summary>
    /// Статический метод расширение интерфейса IApplicationBuilder,
    /// добавление  middleware типа ExceptionMiddleware в объект builder
    /// типа IApplicationBuilder, для обработки ошибок во время выполнение приложений
    /// </summary>
    /// <param name="builder">builder, это, объект Интерфейса IApplicationBuilder</param>
    /// <returns>Возвращает метод расширение интерфейса IApplicationBuilder</returns>
    public static IApplicationBuilder UseErrorException(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionMiddleware>();
    }
    #endregion
}