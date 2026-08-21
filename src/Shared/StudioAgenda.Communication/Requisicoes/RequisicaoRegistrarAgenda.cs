using System.Text.Json.Serialization;
using StudioAgenda.Domain.Entidades;

namespace StudioAgenda.Domain.Dtos.Requisicoes;

public class RequisicaoRegistrarAgenda
{
    public Guid ClienteId { get; set; }
   

    public Guid ProfissionalId { get; set; }
   
    public DateTime DataHora { get; set; }
    public string? Servico { get; set; }
    public decimal? Valor { get; set; }
}