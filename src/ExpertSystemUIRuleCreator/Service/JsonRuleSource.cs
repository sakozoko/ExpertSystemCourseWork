using System;
using System.IO;
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
        var sr = new StreamReader(_path).BaseStream;
        var rules = await JsonSerializer.DeserializeAsync<JsonRule[]?>(sr) ?? Array.Empty<JsonRule>();
        await sr.DisposeAsync();
        var rulesList = rules.AppendToStart(model);
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