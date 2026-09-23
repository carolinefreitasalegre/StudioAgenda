using StudioAgenda.Communication.Respostas;

namespace StudioAgenda.Application.UseCases.Profissional.Perfil;

public interface IPerfilProfissionalUseCase
{
    Task<RespostaPerfilUsuarioJson> Execute();
}