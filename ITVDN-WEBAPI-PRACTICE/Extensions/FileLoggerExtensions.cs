using ITVDN_WEBAPI_PRACTICE.Loggers;
namespace ITVDN_WEBAPI_PRACTICE.Extensions;

public static class FileLoggerExtensions
{
    #region METHOD
    /// <summary>
    /// Статический метод расширение интерфейса ILoggingBuilder
    /// добавление типа FileLoggerProvider в объект builder
    /// типа ILoggingBuilder, для добавление логгирование в текстовой файл
    /// </summary>
    /// <param name="builder">builder это, объект интерфейса ILoggingBuilder</param>
    /// <param name="filePath">filePath это, путь к файлу</param>
    /// <returns>Возвращает метод расширение интерфейса ILoggingBuilder</returns>
    public static ILoggingBuilder AddFile(this ILoggingBuilder builder, string filePath) => 
        builder.AddProvider(new FileLoggerProvider(filePath));
    #endregion
}