using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ExpertSystem.Models;
using ExpertSystemUIRuleCreator.Extension;
using ExpertSystemUIRuleCreator.Interfaces;

namespace ExpertSystemUIRuleCreator.Service;

public class JsonRuleSource : IRuleSource
{
    private readonly string _path;

    public JsonRuleSource(string path)
    {
        _path = path;
        CreateFileIfNotExist();
    }

    public async Task Add(JsonRule model)
    {
        var rules = await GetAll();
        var rulesList = rules.AppendToStart(model).ToList();
        await using var sw = new StreamWriter(_path).BaseStream;
        await JsonSerializer.SerializeAsync(sw, rulesList);
    }

    public async Task<IEnumerable<JsonRule>> GetAll()
    {
        await using var sr = new StreamReader(_path).BaseStream;
        var rules = await JsonSerializer.DeserializeAsync<JsonRule[]>(sr).ConfigureAwait(false) ??
                    Array.Empty<JsonRule>();
        return rules;
    }

    public async Task Remove(JsonRule model)
    {
        //remove model from json file
        var rules = await GetAll();
        var rulesList = rules.ToList();
        rulesList.Remove(rulesList.FirstOrDefault(c => c.Name?.Equals(model.Name) ?? false) ?? new JsonRule());
        await using var sw = new StreamWriter(_path).BaseStream;
        await JsonSerializer.SerializeAsync(sw, rulesList);
    }

    private void CreateFileIfNotExist()
    {
        if (File.Exists(_path)) return;
        using var sr = File.Open(_path, FileMode.OpenOrCreate);
        sr.Write(Encoding.ASCII.GetBytes("[]"));
    }
}