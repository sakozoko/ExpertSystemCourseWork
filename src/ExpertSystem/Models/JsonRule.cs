using System.Collections.Generic;

namespace ExpertSystem.Models;

public class JsonRule
{
    public JsonRule()
    {
        Antecedent = new List<JsonClause>();
    }

    public string? Name { get; set; }
    public List<JsonClause> Antecedent { get; set; }
    public JsonClause? Consequent { get; set; }
}