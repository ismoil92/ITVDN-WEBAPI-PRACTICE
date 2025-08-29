namespace ClassLibrary.Data.Models;

public class PitStop
{
    public int Id { get; set; }
    public string? PitStopCity {  get; set; }
    public string? PitStopDuration {  get; set; }
    public int? AirportId { get; set; }
    public int? DepAndArrId { get; set; }
    public Airport? Airport { get; set; }
    public DepartureAndArrival? DepartureAndArrival { get; set; }
}