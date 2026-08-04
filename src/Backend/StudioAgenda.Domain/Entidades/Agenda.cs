using StudioAgenda.Domain.Enums;

namespace StudioAgenda.Domain.Entidades;

public class Agenda
{
    public Guid Id { get; set; }

    public Guid ClienteId { get; set; }
    public virtual Cliente Cliente { get; set; } 

    public Guid ProfissionalId { get; set; }
    public virtual Profissional Profissional { get; set; } 

    public DateTime DataHora { get; set; }

    public string Servico { get; set; }
    public decimal Valor { get; set; }

    public EStatusAgendamento Status { get; set; } = EStatusAgendamento.Aberto;
    
    public DateTime DataCriacao { get; set; } = DateTime.Now;
}