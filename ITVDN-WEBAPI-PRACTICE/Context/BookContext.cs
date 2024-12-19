using ITVDN_WEBAPI_PRACTICE.Models;
using Microsoft.EntityFrameworkCore;
namespace ITVDN_WEBAPI_PRACTICE.Context;

public class BookContext : DbContext
{
    #region CONSTUCTORS
    public BookContext() { }
    public BookContext(DbContextOptions<BookContext> options) : base(options)
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    #endregion

    #region PROPERTY
    public DbSet<Book> Books { get; set; }
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
        modelBuilder.Entity<Book>(entity =>
        {
            entity.Property("Id");
            entity.HasKey("Id");

            entity.Property("Name").IsRequired();
            entity.Property("Genre").IsRequired();
            entity.Property("Author").IsRequired();
            entity.Property("Price").IsRequired();

            entity.ToTable("Books");
        });
    }

    #endregion
}