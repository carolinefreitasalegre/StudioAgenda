using StudioAgenda.Domain.Enums;

namespace StudioAgenda.Domain.Entidades;

public class Agenda : Entity
{
    public Guid ClienteId { get; set; }
    public virtual Cliente Cliente { get; set; } 

    public Guid ProfissionalId { get; set; }
    public virtual Profissional Profissional { get; set; } 
    
    public DateOnly Data { get; set; }
    public TimeOnly HoraInicio { get; set; }
    
    public EServicos? Servico { get; set; }
    public decimal? Valor { get; set; }

    public EStatusAgendamento Status { get; set; } = EStatusAgendamento.Aberto;
    
    public DateTime DataCriacao { get; set; } = DateTime.Now;
    public TimeOnly HoraFim { get; set; }
}