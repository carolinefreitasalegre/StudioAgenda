using StudioAgenda.Domain.Dtos.Requisicoes;

namespace StudioAgenda.Application.UseCases.Agenda.CancelarAgenda;

public interface ICancelarAgendaUseCase
{
   Task Execute(RequisicaoCancelarHorario id);
}