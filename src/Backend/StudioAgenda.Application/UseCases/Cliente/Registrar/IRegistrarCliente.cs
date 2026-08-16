using StudioAgenda.Communication.Respostas;
using StudioAgenda.Domain.Dtos.Requisicoes;

namespace StudioAgenda.Application.UseCases.Cliente;

public interface IRegistrarCliente
{
    Task<RespostaRegistroUsuarioJson> Execute(RequisicaoRegistrarCliente dados);
}