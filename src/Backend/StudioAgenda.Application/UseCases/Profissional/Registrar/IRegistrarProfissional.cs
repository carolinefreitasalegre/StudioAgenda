using StudioAgenda.Communication.Respostas;
using StudioAgenda.Domain.Dtos.Requisicoes;

namespace StudioAgenda.Application.UseCases.Profissional.Registrar;

public interface IRegistrarProfissional
{
    Task<RespostaRegistroProfissionalJson> Execute(RequisicaoRegistrarProfissional dados);

}