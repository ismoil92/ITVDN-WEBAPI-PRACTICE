namespace ITVDN_WEBAPI_PRACTICE.Middlewares;

public class ExceptionMiddleware
{
    #region FIELD
    private readonly RequestDelegate next;
    #endregion


    #region CONSTUCTOR
    public ExceptionMiddleware(RequestDelegate next)
    {
        this.next = next;
    }
    #endregion


    #region METHOD
    /// <summary>
    /// Метод, для записи исключении в текстовой файл
    /// </summary>
    /// <param name="context">context объект типа HttpContext</param>
    /// <returns>Возвращает объект типа Task</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path;
        var time = DateTime.Now;

        if(path.Equals("/error-development"))
        {
            File.AppendAllText("exceptionLogger.txt", $"Error-development application. Date: {time.ToShortDateString()}---Time: {time.ToShortTimeString()}\n\n");
        }
        else if(path.Equals("/error"))
        {
            File.AppendAllText("exceptionLogger.txt", $"Error application. Date: {time.ToShortDateString()}---Time: {time.ToShortTimeString()}\n\n");
        }
        else
        {
            await next.Invoke(context);
        }
    }
    #endregion
}