using ClassLibrary.Data.Models;
using ClassLibrary.Data.Repositories;
using ITVDN_WEBAPI_PRACTICE.Extensions;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web API", Version = "v1" });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    c.IncludeXmlComments(xmlPath);
});
builder.Services.AddLogging(logger => logger.ClearProviders());
builder.Logging.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "logger.txt"));


var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });

    app.UseSwagger();

    app.UseExceptionHandler("/ErrorHandler/ErrorDevelopment");
}

else
{
    app.UseExceptionHandler("/ErrorHandler/ErrorDevelopment");
}


app.MapControllers();
app.Run();
