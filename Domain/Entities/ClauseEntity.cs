namespace Domain.Entities;

public class ClauseEntity : BaseEntity
{
    public string? Variable { get; set; }
    public string Condition { get; set; } = "=";
    public string? Value { get; set; }
}

