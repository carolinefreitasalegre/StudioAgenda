namespace StudioAgenda.Domain.Repositorios.Profissional;

public interface IRegistrarProfissionalRepository
{
    Task RegistrarProfissional(Entidades.Profissional profissional);
}