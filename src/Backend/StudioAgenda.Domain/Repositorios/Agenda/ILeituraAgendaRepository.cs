namespace StudioAgenda.Domain.Repositorios.Agenda;

public interface ILeituraAgendaRepository
{
    Task<bool?> ExisteAegendaHoje(DateTime hoje);

}