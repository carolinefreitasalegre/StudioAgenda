using StudioAgenda.Domain.Enums;

namespace StudioAgenda.Domain.Dtos.Requisicoes;

public class RequisicaoEdicaoAgendaClienteJson
{
    public DateOnly Data { get; set; }
    public TimeOnly HoraInicio { get; set; } 
    //public TimeOnly HoraFim { get; set; }
    public EServicos Servico { get; set; }
    public EStatusAgendamento Status { get; set; }
}