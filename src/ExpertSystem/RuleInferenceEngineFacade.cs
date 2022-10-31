using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using chen0040.ExpertSystem;
using ExpertSystem.Extension;
using ExpertSystem.Models;

namespace ExpertSystem;

public class RuleInferenceEngineFacade
{
    public RuleInferenceEngineFacade()
    {
        Engine = new RuleInferenceEngine();
        Variables = new List<string>();
        Facts = new List<Clause?>();
        PossibleConditions = new[] { "=", "<", ">" };
    }

    public RuleInferenceEngine Engine { get; }
    public List<string> Variables { get; }
    public List<Clause?> Facts { get; }

    public string[] PossibleConditions { get; }

    private JsonRule[] DeserializeFromJsonFileToJsonRuleArray(string path)
    {
        using var stream = new StreamReader(path).BaseStream;
        return JsonSerializer.Deserialize<JsonRule[]>(stream) ?? Array.Empty<JsonRule>();
    }


    public RuleInferenceEngineFacade SetFacts(IEnumerable<Clause?> clauses)
    {
        foreach (var clause in clauses)
        {
            if (Facts.Select(c => c?.Variable).Contains(clause?.Variable))
            {
                Facts.RemoveAt(
                    Facts.IndexOf(
                        Facts.First(
                            c => c?.Variable.Equals(clause?.Variable) ?? false
                        )
                    )
                );
                var tempArr = new Clause?[Facts.Count];
                Facts.CopyTo(tempArr);
                Facts.Clear();
                Engine.ClearFacts();
                SetFacts(tempArr);
            }

            Engine.AddFact(clause);
            Facts.Add(clause);
        }

        return this;
    }


    public RuleInferenceEngineFacade SetFacts(IEnumerable<(string? Variable, string? Condition, string? Value)> facts)
    {
        return SetFacts(facts
            .Where(x => !string.IsNullOrWhiteSpace(x.Variable) && !string.IsNullOrWhiteSpace(x.Value) &&
                        !string.IsNullOrWhiteSpace(x.Value))
            .Select(c => (c.Variable, c.Condition, c.Value).MapTupleClauseToClause()));
    }

    public string GetResult(string variable)
    {
        var list = new List<Clause>();
        var result = Engine.Infer(variable, list);
        return result?.ToString() ?? "Rule is not found";
    }

    public RuleInferenceEngineFacade ClearFacts()
    {
        Engine.ClearFacts();
        Facts.Clear();
        return this;
    }

    public Rule MapJsonRuleToRuleAndLog(JsonRule jsonRule)
    {
        foreach (var jsonClause in jsonRule.Antecedent.Where(jsonClause =>
                     !string.IsNullOrWhiteSpace(jsonClause.Variable) && !Variables.Contains(jsonClause.Variable)))
            Variables.Add(jsonClause.Variable!);

        return jsonRule.MapJsonRuleToRule();
    }


    public RuleInferenceEngineFacade SetKnowledgeBase(string path)
    {
        var jsonRules = DeserializeFromJsonFileToJsonRuleArray(path);
        var rulesList = new List<Rule>(jsonRules.Length);
        rulesList.AddRange(jsonRules.Select(MapJsonRuleToRuleAndLog));
        return SetKnowledgeBase(rulesList);
    }

    public RuleInferenceEngineFacade SetKnowledgeBase(IEnumerable<Rule> rules)
    {
        foreach (var rule in rules)
            Engine.AddRule(rule);
        return this;
    }
}