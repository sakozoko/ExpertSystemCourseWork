namespace Domain.Entities;

public class ClauseEntity : BaseEntity
{
    public string Variable { get; set; } = string.Empty;
    public string Condition { get; set; } = "=";
    public string Value { get; set; } = string.Empty;
}

