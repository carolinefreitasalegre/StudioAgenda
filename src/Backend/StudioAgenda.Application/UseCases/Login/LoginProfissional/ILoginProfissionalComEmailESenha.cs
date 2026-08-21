using StudioAgenda.Communication.Respostas;
using StudioAgenda.Domain.Dtos.Requisicoes;

namespace StudioAgenda.Application.UseCases.Login;

public interface ILoginProfissionalComEmailESenha
{
    Task<RespostaRegistroProfissionalJson>Execute(RequisicaoProfissionalLoginJson requisicaoProfissional);
}