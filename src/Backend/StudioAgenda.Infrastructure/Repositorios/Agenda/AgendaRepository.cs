using Microsoft.EntityFrameworkCore;
using StudioAgenda.Domain.Repositorios.Agenda;

namespace StudioAgenda.Infrastructure.Repositorios.Agenda;

internal class AgendaRepository : IRegistrarAgendaRepository, ILeituraAgendaRepository
{
    private readonly StudioAgendaDbContext _context;

    public AgendaRepository(StudioAgendaDbContext context)
    {
        _context = context;
    }

    public async Task RegistrarAgenda(Domain.Entidades.Agenda agenda)
    {
        await _context.agendas.AddAsync(agenda);
    }

    public async Task<bool?> ExisteAegendaHoje(DateTime hoje)
    {
        return await _context.agendas.AnyAsync();
    }

    public async Task<IReadOnlyList<Domain.Entidades.Agenda?>> Agenda(Guid id)
    {
        return await _context.agendas.Where(x => x.ClienteId == id).ToListAsync();
    }
}