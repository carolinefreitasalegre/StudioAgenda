namespace StudioAgenda.Domain.Repositorios.Agenda;

public interface ILeituraAgendaRepository
{
    Task<bool> ExisteAegendaHoje(DateTime hoje);
    Task<bool> HorariosReservados(TimeOnly hoje);
    Task<IReadOnlyList<Entidades.Agenda?>> AgendaPorId(Guid id);
    Task<IReadOnlyList<Domain.Entidades.Agenda>> AgendaGeral();
    Task<bool> ExisteConflito(TimeOnly inicio, TimeOnly fim);
    Task<IReadOnlyList<Domain.Entidades.Agenda>> AgendaPorIdProfissional(Guid id);
}