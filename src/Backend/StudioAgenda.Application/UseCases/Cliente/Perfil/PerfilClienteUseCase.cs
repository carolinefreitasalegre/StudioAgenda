using Mapster;
using StudioAgenda.Communication.Respostas;
using StudioAgenda.Domain.Identidade;

namespace StudioAgenda.Application.UseCases.Cliente.Perfil;

public class PerfilClienteUseCase : IPerfilClienteUseCase
{
    private readonly IUsuarioLogado _usuarioLogado;

    public PerfilClienteUseCase(IUsuarioLogado usuarioLogado)
    {
        _usuarioLogado = usuarioLogado;
    }

    public async Task<RespostaPerfilUsuario> Execute()
    {
        var usuarioLogado = await _usuarioLogado.PegarCliente();
        return usuarioLogado.Adapt<RespostaPerfilUsuario>();
    }
}