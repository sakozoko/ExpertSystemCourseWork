using System.Threading.Tasks;
using ExpertSystem.Models;

namespace ExpertSystemUIRuleCreator.Interfaces;

public interface IRuleSource
{
    Task Add(JsonRule model);
}