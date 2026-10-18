using Microsoft.EntityFrameworkCore;
using StudioAgenda.Domain.Enums;
using StudioAgenda.Domain.Repositorios.Agenda;

namespace StudioAgenda.Infrastructure.Repositorios.Agenda;

internal class AgendaRepository : IRegistrarAgendaRepository, ILeituraAgendaRepository, IEditarAgendaClienteRepository, ICancelarHorarioRepository
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
    
    public async Task<List<Domain.Entidades.Agenda?>> AgendaPorIdCliente(Guid id)
    {
        return await _context.agendas.ToListAsync();
    }

    public async Task<Domain.Entidades.Agenda> AgendaPorId(Guid id,  Guid usuarioId)
    {
        return await _context.agendas.FirstOrDefaultAsync(agenda => agenda.Id == id && agenda.ClienteId == usuarioId);
    }

    public async Task<IReadOnlyList<Domain.Entidades.Agenda>> AgendaGeral()
    {
        return await _context.agendas.ToListAsync();
    }
    
    public async Task<bool> ExisteConflito(DateOnly data, TimeOnly inicio, TimeOnly fim, EStatusAgendamento stats)
    {
        return await _context.agendas
            .AnyAsync(a => inicio < a.HoraFim && fim > a.HoraInicio && a.Data == data && a.Status != EStatusAgendamento.Cancelado);
    }

    public async Task<IReadOnlyList<Domain.Entidades.Agenda>> AgendaPorIdProfissional(Guid id)
    {
        return await _context.agendas.Where(profissional => profissional.ProfissionalId == id).ToListAsync();
    }

    public void EditarAgendaCliente(Domain.Entidades.Agenda agenda)
    {
        _context.agendas.Update(agenda);
    }
    
    public async Task CanelarHorario(Domain.Entidades.Agenda agenda)
    {
         _context.agendas.Update(agenda);
    }
   
}