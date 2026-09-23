using StudioAgenda.Communication.Respostas;

namespace StudioAgenda.Application.UseCases.Cliente.Perfil;

public interface IPerfilClienteUseCase
{
    Task<RespostaPerfilUsuarioJson> Execute();
}