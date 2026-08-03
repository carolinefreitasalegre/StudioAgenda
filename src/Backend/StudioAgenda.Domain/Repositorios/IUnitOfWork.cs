namespace StudioAgenda.Domain.Repositorios;

public interface IUnitOfWork
{
    Task Commit();
}