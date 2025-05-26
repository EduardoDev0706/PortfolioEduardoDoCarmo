using AgendaEletronica.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace AgendaEletronica.Data;

public class AgendaService
{
    private readonly string _caminho = "agenda.json";

    // Le um arquivo chamado agenda.json, transforma em uma lista Compromisso
    // e retorna
    public List<Compromisso> Carregar()
    {
        if (!File.Exists(_caminho))
            return new List<Compromisso>();

        var json = File.ReadAllText(_caminho);
        return JsonSerializer.Deserialize<List<Compromisso>>(json) ?? new();
    }

    // Pega uma lista de compromissos e grava na agenda.json
    public void Salvar(List<Compromisso> compromissos)
    {
        var json = JsonSerializer.Serialize(compromissos, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_caminho, json);
    }
}