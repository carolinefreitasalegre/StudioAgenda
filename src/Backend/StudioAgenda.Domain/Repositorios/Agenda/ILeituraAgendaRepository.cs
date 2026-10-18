using StudioAgenda.Domain.Enums;

namespace StudioAgenda.Domain.Repositorios.Agenda;

public interface ILeituraAgendaRepository
{
    Task<bool> ExisteAegendaHoje(DateTime hoje);
    Task<bool> HorariosReservados(TimeOnly hoje);
    Task<List<Entidades.Agenda?>> AgendaPorIdCliente(Guid id);
    Task<Entidades.Agenda?> AgendaPorId(Guid id, Guid usuarioId);
    Task<IReadOnlyList<Domain.Entidades.Agenda>> AgendaGeral();
    Task<bool> ExisteConflito(DateOnly data, TimeOnly inicio, TimeOnly fim, EStatusAgendamento status);
    Task<IReadOnlyList<Domain.Entidades.Agenda>> AgendaPorIdProfissional(Guid id);
}