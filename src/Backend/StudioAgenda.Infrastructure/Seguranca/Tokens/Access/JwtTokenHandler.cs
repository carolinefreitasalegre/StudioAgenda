using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using StudioAgenda.Domain.Entidades;
using StudioAgenda.Domain.Seguranca.Tokens;

namespace StudioAgenda.Infrastructure.Seguranca.Tokens;

internal sealed class JwtTokenHandler : IAccessTokenGernerator
{
    private readonly uint _expirationTimeMinutes;
    private readonly string _signinKey;

    public JwtTokenHandler(uint expirationTimeMinutes, string signinKey)
    {
        _expirationTimeMinutes = expirationTimeMinutes;
        _signinKey = signinKey;
    }

    public string Generator(UsuarioBase usuario)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
        };
        
        var tokendescription = new SecurityTokenDescriptor
        {
            Expires =  DateTime.UtcNow.AddMinutes(_expirationTimeMinutes),
            SigningCredentials = new SigningCredentials(Credentials(), SecurityAlgorithms.HmacSha256Signature),
            Subject = new ClaimsIdentity(claims)
        };

        var handler = new JsonWebTokenHandler();
        return handler.CreateToken(tokendescription);
    }

    private SymmetricSecurityKey Credentials()
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_signinKey));
    }
}