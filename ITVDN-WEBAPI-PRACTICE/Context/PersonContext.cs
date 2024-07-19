using ITVDN_WEBAPI_PRACTICE.Models;
using Microsoft.EntityFrameworkCore;

namespace ITVDN_WEBAPI_PRACTICE.Context;

public class PersonContext : DbContext
{
    #region CONSTRUCTORS
    public PersonContext() { }
    public PersonContext(DbContextOptions<PersonContext> options) : base(options)
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    #endregion

    #region PROPERTY
    public DbSet<Person> People { get; set; }
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
        optionsBuilder.UseSqlServer("Server=HP-NETBOOK-WIN\\SQLEXPRESS;Database=itvdnDB;User Id=itvdn;Password=1;Trusted_Connection=True;TrustServerCertificate=True");
    }


    /// <summary>
    /// Переопределенный метод, Для создание таблицы в базе данных с помощью моделей
    /// </summary>
    /// <param name="modelBuilder">modelBuilder тип класса является ModelBuilder, создаёт таблицы в базу данных с помощью моделей</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(entity =>
        {
            entity.Property("Id");
            entity.HasKey(p => p.Id);

            entity.Property("Name").IsRequired();
            entity.Property("Age").IsRequired();

            entity.ToTable("People");
        });
    }

    #endregion
}