namespace ClassLibrary.Data.Models;

public class CodeAirport
{
    public int Id { get; set; }
    public string? Type {  get; set; }
    public string? Code {  get; set; }

    public int? AirportId { get; set; }
    public Airport? Airport { get; set; } 
}