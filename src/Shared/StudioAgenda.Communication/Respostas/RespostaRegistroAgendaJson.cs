using StudioAgenda.Domain.Enums;

namespace StudioAgenda.Communication.Respostas;

public class RespostaRegistroAgendaJson
{
    public Guid ClienteId { get; set; }

    public Guid ProfissionalId { get; set; }

    public DateOnly Data { get; set; }
    public TimeOnly HoraInicio { get; set; }

    public TimeOnly HoraFim { get; set; }

    public EServicos? Servico { get; set; }
    public decimal? Valor { get; set; }

    public EStatusAgendamento Status { get; set; } = EStatusAgendamento.Aberto;
    
    public DateTime DataCriacao { get; set; } = DateTime.Now;
}