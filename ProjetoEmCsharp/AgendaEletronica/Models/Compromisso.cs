using System;

namespace AgendaEletronica.Models;

public class Compromisso
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime Data { get; set; } = DateTime.Today;
}