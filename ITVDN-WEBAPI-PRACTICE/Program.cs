using ITVDN_WEBAPI_PRACTICE.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging(logging => logging.ClearProviders());
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
    app.UseExceptionHandler("/ErrorHandler/Error");
}

app.MapControllers();
app.Run();