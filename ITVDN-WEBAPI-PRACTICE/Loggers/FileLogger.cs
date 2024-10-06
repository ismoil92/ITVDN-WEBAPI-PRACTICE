namespace ITVDN_WEBAPI_PRACTICE.Loggers;

public class FileLogger : ILogger, IDisposable
{
    #region FIELDS
    private readonly string filePath;
    static object _lock = new object();
    #endregion

    #region CONSTUCTOR
    public FileLogger(string filePath) => this.filePath = filePath;
    #endregion

    #region METHODS
    /// <summary>
    /// Обобщенный метод, который представляет некоторую область видимости для логгера
    /// </summary>
    /// <typeparam name="TState">Это объект состояния, который хранить сообщение</typeparam>
    /// <param name="state">объект типа TState</param>
    /// <returns>Возвращает объект интерфейса IDisposable</returns>
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => this;

    /// <summary>
    /// Метод, для вызова сборщика мусора (Garbage Collector)
    /// </summary>
    public void Dispose() { }

    /// <summary>
    /// Метод, указывает доступен ли логгер для использования
    /// </summary>
    /// <param name="logLevel">logLevel, этот объект типа Loglevel, чтобы задействовать логгер</param>
    /// <returns>возаращает булевое значение, задействован логгер или нет</returns>
    public bool IsEnabled(LogLevel logLevel) => true;


    /// <summary>
    /// Метод, для записи логгирование в текстовой файл
    /// </summary>
    /// <typeparam name="TState">Это объект состояния, который хранить сообщение</typeparam>
    /// <param name="logLevel">logLevel, этот объект типа Loglevel, чтобы задействовать логгер</param>
    /// <param name="eventId"></param>
    /// <param name="state">state,этот объект типа TState, который зранить сообщение</param>
    /// <param name="exception">exception, этот объект типа Exception, для получение исключений и записать в текстовой файл</param>
    /// <param name="formatter">formatter, этот объект обобщенного делегата тип, который 
    /// Func<TState, Exception?, string>, для получение информации об ошибке и запись в текстовой файл</param>
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        lock (_lock)
        {
            if (exception != null)
            {
                var time = DateTime.Now;
                string? message = $"Time:{time.ToShortTimeString()}.Stack Trace:{exception!.StackTrace}\n\nMessage:{exception.Message}\n=====================================================================================\n\n";
                File.AppendAllText(filePath, formatter(state, exception!) + Environment.NewLine + message);
            }
        }
    }

    #endregion
}