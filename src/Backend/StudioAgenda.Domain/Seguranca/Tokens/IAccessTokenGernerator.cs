using StudioAgenda.Domain.Entidades;

namespace StudioAgenda.Domain.Seguranca.Tokens;

public interface IAccessTokenGernerator
{
    string Generator(UsuarioBase usuario);
}