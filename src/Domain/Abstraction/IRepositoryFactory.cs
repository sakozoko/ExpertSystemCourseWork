namespace Domain.Abstraction;

public interface IRepositoryFactory
{
    public IClauseRepository CreateClauseRepository();
    public IRuleRepository CreateRuleRepository();
}