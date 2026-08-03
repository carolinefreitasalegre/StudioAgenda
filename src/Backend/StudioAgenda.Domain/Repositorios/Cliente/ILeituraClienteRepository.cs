using StudioAgenda.Domain.Entidades;

namespace StudioAgenda.Domain.Repositorios;

public interface ILeituraClienteRepository
{
    Task<bool> ExisteUsuarioAtivoTelefone(string telefone);
    Task<bool> ExisteUsuarioAtivoId(Guid id);
    Task<Cliente?> ObterViaTelefone(string telefone);
}