namespace ClassLibrary.Data.Models;

public class DepartureAndArrival
{
    public int Id { get; set; }
    public string? Departure {  get; set; }
    public string? Arrival { get; set; }
    public string? Routing {  get; set; }
    public int Distance {  get; set; }
    public int? AirportId { get; set; }
    public Airport? Airport { get; set; }
    public List<PitStop> PitStops { get; set; }

    public DepartureAndArrival()
    {
        PitStops = new List<PitStop>();
    }
}