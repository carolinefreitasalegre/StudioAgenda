namespace StudioAgenda.Domain.Repositorios.Agenda;

public interface IRegistrarAgendaRepository
{
    Task RegistrarAgenda(Entidades.Agenda agenda);
}