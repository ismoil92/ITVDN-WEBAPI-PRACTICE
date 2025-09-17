namespace ClassLibrary.Data.Models;

public class Weather
{
    public int Id { get; set; }
    public string? Status {  get; set; }
    public float Temp {  get; set; }
    public float MinTemp {  get; set; }
    public float MaxTemp { get; set; }
}