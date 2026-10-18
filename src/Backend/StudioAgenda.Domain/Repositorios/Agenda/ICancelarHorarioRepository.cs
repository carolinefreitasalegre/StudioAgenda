namespace StudioAgenda.Domain.Repositorios.Agenda;

public interface ICancelarHorarioRepository
{
    Task CanelarHorario(Domain.Entidades.Agenda agenda);
}