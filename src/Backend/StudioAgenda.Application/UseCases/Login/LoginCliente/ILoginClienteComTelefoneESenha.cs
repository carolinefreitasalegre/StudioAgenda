using StudioAgenda.Communication.Respostas;
using StudioAgenda.Domain.Dtos.Requisicoes;

namespace StudioAgenda.Application.UseCases.Login.LoginCliente;

public interface ILoginClienteComTelefoneESenha
{
    Task<RespostaRegistroClienteJson> Execute(RequisicaoClienteLoginJson requisicao);
}