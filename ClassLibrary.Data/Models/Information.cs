namespace ClassLibrary.Data.Models;

public class Information
{
    public int Id { get; set; }
    public string? NumberFlight {  get; set; }
    public string? NumberBoard {  get; set; }
    public int? AirportId { get; set; }
    public Airport? Airport { get; set; }
}