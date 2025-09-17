namespace ITVDN_WEBAPI_PRACTICE.Loggers;

public class FileLoggerProvider : ILoggerProvider
{
    #region FIELD
    private string _filePath;
    #endregion

    #region CONSTRUCTOR
    public FileLoggerProvider(string filePath) => this._filePath = filePath;
    #endregion


    #region METHODS
    /// <summary>
    /// Метод, для создание файлового логгера. 
    /// </summary>
    /// <param name="categoryName">categoryName? этот объект типа String</param>
    /// <returns>Возвращает объект типа FileLogger, т.е. Файловой логгер</returns>
    public ILogger CreateLogger(string categoryName) => new FileLogger(_filePath);

    /// <summary>
    /// Метод, для вызова сборщика мусора (Garbage Collector)
    /// </summary>
    public void Dispose() { }
    #endregion
}