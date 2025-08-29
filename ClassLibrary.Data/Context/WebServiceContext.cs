using ClassLibrary.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary.Data.Context;

public class WebServiceContext : DbContext
{

    #region CONSTUCTORS
    public WebServiceContext() { }

    public WebServiceContext(DbContextOptions<WebServiceContext> contextOptions) : base(contextOptions)
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    #endregion

    public DbSet<Airport> Airports { get; set; }
    public DbSet<CodeAirport> CodeAirports { get; set; }
    public DbSet<Information> Informations { get; set; }
    public DbSet<PitStop> PitStops { get; set; }
    public DbSet<DepartureAndArrival> DeparturesAndArrivals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=HP-NETBOOK-WIN\\SQLEXPRESS;Database=itvdnDB;User id=itvdn;Password=1;Trusted_Connection=True;TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {



        // Airports Table
        modelBuilder.Entity<Airport>(entity =>
        {
            entity.Property("Id");
            entity.HasKey("Id");

            entity.Property("Country").IsRequired();
            entity.Property("City").IsRequired();
            entity.Property("NameAirport").IsRequired();
            entity.Property("Airline").IsRequired();
            entity.Property("Active").IsRequired();


            //one-to-one. Airport-Information
            entity.HasOne(x => x.Information)
            .WithOne(x => x.Airport)
            .HasForeignKey<Information>(x => x.AirportId);


            //one-to-many. Airport-CodeAirport
            entity.HasMany(x => x.CodeAirports)
            .WithOne(x => x.Airport)
            .HasForeignKey(x => x.AirportId);


            //one-to-many. Airport-PitStop
            entity.HasMany(x => x.PitStops)
            .WithOne(x => x.Airport)
            .HasForeignKey(x => x.AirportId);


            //one-to-many. Airport-DepartureAndArrival
            entity.HasMany(x => x.DepartureAndArrivals)
            .WithOne(x => x.Airport)
            .HasForeignKey(x => x.AirportId);


            entity.ToTable("Airports");
        });


        //DeparturesAndArrivals Table
        modelBuilder.Entity<DepartureAndArrival>(entity =>
        {
            entity.Property("Id");
            entity.HasKey("Id");

            entity.Property("Departure").IsRequired();
            entity.Property("Arrival").IsRequired();
            entity.Property("Routing").IsRequired();
            entity.Property("Distance").IsRequired();


            //one-to-many. DepartureAndArrival-PitStop
            entity.HasMany(x => x.PitStops)
            .WithOne(x => x.DepartureAndArrival)
            .HasForeignKey(x => x.DepAndArrId);

            entity.ToTable("DeparturesAndArrivals");
        });


        //CodeAirports Table
        modelBuilder.Entity<CodeAirport>(entity =>
        {
            entity.Property("Id");
            entity.HasKey("Id");

            entity.Property("Type").IsRequired();
            entity.Property("Code").IsRequired();

            entity.ToTable("CodeAirports");
        });



        //PitStops Table
        modelBuilder.Entity<PitStop>(entity =>
        {
            entity.Property("Id");
            entity.HasKey("Id");

            entity.Property("PitStopCity").IsRequired();
            entity.Property("PitStopDuration").IsRequired();

            entity.ToTable("PitStops");
        });


        //Informations Table
        modelBuilder.Entity<Information>(entity =>
        {
            entity.Property("Id");
            entity.HasKey("Id");

            entity.Property("NumberFlight").IsRequired();
            entity.Property("NumberBoard").IsRequired();

            entity.ToTable("Informations");
        });
    }
}