using StudioAgenda.Communication.Respostas;

namespace StudioAgenda.Application.UseCases.Agenda.LeituraAgenda;

public interface ILeituraAgendaPorClienteUseCase
{
    Task<IReadOnlyList<RespostaRegistroAgendaJson>> Execute();
}