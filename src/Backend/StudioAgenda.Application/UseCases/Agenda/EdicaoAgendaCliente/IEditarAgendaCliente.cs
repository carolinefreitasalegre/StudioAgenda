using StudioAgenda.Communication.Respostas;
using StudioAgenda.Domain.Dtos.Requisicoes;

namespace StudioAgenda.Application.UseCases.Agenda.EdicaoAgenaCliente;

public interface IEditarAgendaCliente
{
    Task Execute(RequisicaoEdicaoAgendaCliente dados, Guid agendaId);
}