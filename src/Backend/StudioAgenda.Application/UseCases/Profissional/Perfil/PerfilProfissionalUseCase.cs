using Mapster;
using StudioAgenda.Communication.Respostas;
using StudioAgenda.Domain.Identidade;

namespace StudioAgenda.Application.UseCases.Profissional.Perfil;

public class PerfilProfissionalUseCase : IPerfilProfissionalUseCase
{
    private readonly IUsuarioLogado _usuarioLogado;

    public PerfilProfissionalUseCase(IUsuarioLogado usuarioLogado)
    {
        _usuarioLogado = usuarioLogado;
    }

    public async Task<RespostaPerfilUsuarioJson> Execute()
    {
        var usuarioLogado = await _usuarioLogado.PegarProfissional();
        return usuarioLogado.Adapt<RespostaPerfilUsuarioJson>();
    }
}