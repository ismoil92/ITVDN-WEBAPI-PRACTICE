namespace ClassLibrary.Data.Models;

public class Airport
{
    public int Id { get; set; }
    public string? Country {  get; set; }
    public string? City {  get; set; }
    public string? NameAirport {  get; set; }
    public string? Airline {  get; set; }
    public bool Active {  get; set; }
    public Information? Information { get; set; }
    public List<CodeAirport> CodeAirports { get; set; }
    public List<PitStop> PitStops { get; set; }
    public List<DepartureAndArrival> DepartureAndArrivals { get; set; }

    public Airport()
    {
        CodeAirports = new List<CodeAirport>();
        PitStops = new List<PitStop>();
        DepartureAndArrivals = new List<DepartureAndArrival>();
    }
}