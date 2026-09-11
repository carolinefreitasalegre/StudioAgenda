using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using StudioAgenda.Domain.Entidades;
using StudioAgenda.Domain.Identidade;
using StudioAgenda.Domain.Seguranca.Tokens;

namespace StudioAgenda.Infrastructure.Identidade;

internal sealed class UsuarioLogado : IUsuarioLogado
{
    private readonly IAccessTokenProvider _accessTokenProvider;
    private readonly StudioAgendaDbContext _context;

    public UsuarioLogado(IAccessTokenProvider accessTokenProvider, StudioAgendaDbContext context)
    {
        _accessTokenProvider = accessTokenProvider;
        _context = context;
    }

    public async Task<Cliente> PegarCliente()
    {
        var clientId = PegarUsuarioLogado();
        return await _context.clientes.FirstAsync(cliente => cliente.Id == clientId);
    }

    public async Task<Profissional> PegarProfissional()
    {
        var profissionalId = PegarUsuarioLogado();
        return await _context.profissionais.FirstAsync(prof => prof.Id == profissionalId);
    }

    public Guid PegarUsuarioLogado()
    {
        var accessToken = _accessTokenProvider.PegarToken();
        var handler = new JsonWebTokenHandler();
        var jsonWebToken = handler.ReadJsonWebToken(accessToken);
        var subject = jsonWebToken.Claims.First(claim => claim.Type.Equals(JwtRegisteredClaimNames.Sub));
        
        return Guid.Parse(subject.Value);
    }
}