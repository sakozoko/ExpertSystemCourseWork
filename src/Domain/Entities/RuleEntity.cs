namespace Domain.Entities;

public class RuleEntity : BaseEntity
{
    public string Name { get; set; }=string.Empty;
    public ICollection<Antecedent> Antecedents { get; set; }=new List<Antecedent>();
    public Conclusion Conclusion { get; set; } = new();
    
    public int ConclusionId { get; set; }
}