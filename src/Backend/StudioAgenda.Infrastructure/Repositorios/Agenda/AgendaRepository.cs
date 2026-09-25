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

    public async Task<bool> ExisteAegendaHoje(DateTime hoje)
    {
        return await _context.agendas.AnyAsync(agenda => agenda.Data.Equals(DateTime.Today));
    }

    public async Task<bool> HorariosReservados(TimeOnly hora)
    {
        return await _context.agendas.AnyAsync(agenda => agenda.HoraInicio.Equals(hora));
    }
    
    public async Task<IReadOnlyList<Domain.Entidades.Agenda?>> AgendaPorId(Guid id)
    {
        return await _context.agendas.ToListAsync();
    }

    public async Task<IReadOnlyList<Domain.Entidades.Agenda>> AgendaGeral()
    {
        return await _context.agendas.ToListAsync();
    }
    public async Task<bool> ExisteConflito(TimeOnly inicio, TimeOnly fim)
    {
        return await _context.agendas
            .AnyAsync(a => inicio < a.HoraFim && fim > a.HoraInicio);
    }

    public async Task<IReadOnlyList<Domain.Entidades.Agenda>> AgendaPorIdProfissional(Guid id)
    {
        return await _context.agendas.Where(profissional => profissional.ProfissionalId == id).ToListAsync();
       
    }
}