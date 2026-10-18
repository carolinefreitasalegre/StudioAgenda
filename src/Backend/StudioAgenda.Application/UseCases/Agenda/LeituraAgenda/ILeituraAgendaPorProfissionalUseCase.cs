using StudioAgenda.Communication.Respostas;

namespace StudioAgenda.Application.UseCases.Agenda.LeituraAgenda;

public interface ILeituraAgendaPorProfissionalUseCase
{
    Task<IReadOnlyList<RespostaRegistroAgendaJson>> Execute();
}