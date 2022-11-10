using System.Collections.Generic;
using System.Threading.Tasks;
using ExpertSystem.Models;

namespace ExpertSystemUIRuleCreator.Interfaces;

public interface IRuleSource
{
    Task Add(JsonRule model);
    Task<IEnumerable<JsonRule>> GetAll();
    Task Remove(JsonRule model);
}