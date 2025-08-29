using ITVDN_WEBAPI_PRACTICE.Loggers;
namespace ITVDN_WEBAPI_PRACTICE.Extensions;

public static class FileLoggerExtensions
{
    public static ILoggingBuilder AddFile(this ILoggingBuilder builder, string filePath)
        => builder.AddProvider(new FileLoggerProvider(filePath));
}