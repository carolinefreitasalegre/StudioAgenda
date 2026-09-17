namespace StudioAgenda.Domain.Repositorios.Agenda;

public interface ILeituraAgendaRepository
{
    Task<bool?> ExisteAegendaHoje(DateTime hoje);
    Task<IReadOnlyList<Entidades.Agenda?>> Agenda(Guid id);

}