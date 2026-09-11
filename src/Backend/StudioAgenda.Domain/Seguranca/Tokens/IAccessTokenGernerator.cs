using StudioAgenda.Domain.Entidades;

namespace StudioAgenda.Domain.Seguranca.Tokens;

public interface IAccessTokenGernerator
{
    string GeneratorCliente(Cliente cliente);
    string GeneratorProfissional(Profissional profissional);
}