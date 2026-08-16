using StudioAgenda.Domain.Enums;

namespace StudioAgenda.Communication.Respostas;

public class RespostaRegistroAgendaJson
{
    public Guid ClienteId { get; set; }

    public Guid ProfissionalId { get; set; }

    public DateTime DataHora { get; set; }

    public string? Servico { get; set; }
    public decimal? Valor { get; set; }

    public EStatusAgendamento Status { get; set; } = EStatusAgendamento.Aberto;
    
    public DateTime DataCriacao { get; set; } = DateTime.Now;
}