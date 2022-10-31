namespace ExpertSystem.Models;

public class JsonClause
{
    public string? Variable { get; set; }
    public string Condition { get; set; } = "=";
    public string? Value { get; set; }
}