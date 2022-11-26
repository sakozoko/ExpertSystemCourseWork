using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace ExpertSystemUIRuleCreator.Interfaces;

public interface IRuleSource
{
    Task Add(RuleEntity entity);
    Task<IEnumerable<RuleEntity>> GetAll();
    Task Remove(RuleEntity entity);
}