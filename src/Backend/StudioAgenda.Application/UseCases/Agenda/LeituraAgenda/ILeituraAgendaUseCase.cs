using StudioAgenda.Communication.Respostas;

namespace StudioAgenda.Application.UseCases.Agenda.LeituraAgenda;

public interface ILeituraAgendaUseCase
{
    Task<IReadOnlyList<RespostaRegistroAgendaJson>> Execute();
}