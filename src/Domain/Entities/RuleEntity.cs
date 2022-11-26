namespace Domain.Entities;

public class RuleEntity : BaseEntity
{
    public RuleEntity()
    {
        Antecedent = new List<ClauseEntity>();
        Consequent = new ClauseEntity();
    }

    public string? Name { get; set; }
    public List<ClauseEntity> Antecedent { get; set; }
    public ClauseEntity Consequent { get; set; }
}