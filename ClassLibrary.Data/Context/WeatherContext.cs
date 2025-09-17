using ClassLibrary.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary.Data.Context;

public class WeatherContext: DbContext
{
    #region CONTRUCTORS
    public WeatherContext() { }

    public WeatherContext(DbContextOptions<WeatherContext> contextOptions) : base(contextOptions)
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    #endregion

    #region PROPERTY
    public DbSet<Weather> Weathers { get; set; }
    #endregion


    #region METHODS
    /// <summary>
    /// Переопределенный метод, Для конфигурации подключения к серверу (MS SQL Server). 
    /// </summary>
    /// <param name="optionsBuilder">optionsBuilder, тип класса является DbContextOptionsBuilder
    /// вызывает метод UseSqlServer для настройки подключение к серверу. В строку передается концигурация сервера
    /// </param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=HP-NETBOOK-WIN\\SQLEXPRESS;Database=itvdnDB;User id=itvdn;Password=1;Trusted_Connection=True;TrustServerCertificate=True");
    }


    /// <summary>
    /// Переопределенный метод, Для создание таблицы в базе данных с помощью моделей
    /// </summary>
    /// <param name="modelBuilder">modelBuilder тип класса является ModelBuilder, создаёт таблицы в базу данных с помощью моделей</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Weather>(entity =>
        {
            entity.Property("Id");
            entity.HasKey("Id");

            entity.Property("Status").IsRequired();
            entity.Property("Temp").IsRequired();
            entity.Property("MinTemp").IsRequired();
            entity.Property("MaxTemp").IsRequired();

            entity.ToTable("Weathers");
        });
    }
    #endregion
}