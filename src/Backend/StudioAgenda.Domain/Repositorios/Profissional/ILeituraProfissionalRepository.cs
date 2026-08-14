namespace StudioAgenda.Domain.Repositorios.Profissional;

public interface ILeituraProfissionalRepository
{
    Task<bool> ExisteProfissionalAtivoEmail(string email);
    Task<bool> ExisteProfissionalAtivoId(Guid id);
    Task<Entidades.Profissional?> ObterViaEmail(string email);
}