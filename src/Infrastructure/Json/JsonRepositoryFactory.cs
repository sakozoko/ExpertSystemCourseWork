using Domain.Abstraction;
using Infrastructure.Json.Repository;

namespace Infrastructure.Json;

public class JsonRepositoryFactory : IRepositoryFactory
{
    private const string DataFilePath = "knowledgeBase.json";

    public IClauseRepository CreateClauseRepository()
    {
        return new ClauseRepositoryJson(CreateRuleRepository().GetAll().Result);
    }

    public IRuleRepository CreateRuleRepository()
    {
        return new RuleRepositoryJson(DataFilePath);
    }
}