using StudioAgenda.Domain.Entidades;

namespace StudioAgenda.Domain.Identidade;

public interface IUsuarioLogado
{
    Task<Cliente> PegarCliente();
    Task<Profissional> PegarProfissional();
    Guid PegarUsuarioLogado();
}