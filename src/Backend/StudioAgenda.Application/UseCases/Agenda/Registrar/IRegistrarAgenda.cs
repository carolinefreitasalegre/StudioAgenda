using StudioAgenda.Communication.Respostas;
using StudioAgenda.Domain.Dtos.Requisicoes;

namespace StudioAgenda.Application.UseCases.Agenda;

public interface IRegistrarAgenda
{
    Task<RespostaRegistroAgendaJson> Execute(RequisicaoRegistrarAgenda dados);
}