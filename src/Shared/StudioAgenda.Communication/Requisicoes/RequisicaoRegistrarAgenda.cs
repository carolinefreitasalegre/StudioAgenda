using System.Text.Json.Serialization;
using StudioAgenda.Domain.Entidades;
using StudioAgenda.Domain.Enums;

namespace StudioAgenda.Domain.Dtos.Requisicoes;

public class RequisicaoRegistrarAgenda
{
    public Guid ClienteId { get; set; }
    public Guid ProfissionalId { get; set; }
    public DateOnly Data { get; set; }
    public TimeOnly HoraInicio { get; set; } 
    public TimeOnly HoraFim { get; set; }
    public EServicos Servico { get; set; }
    public decimal? Valor { get; set; }
}