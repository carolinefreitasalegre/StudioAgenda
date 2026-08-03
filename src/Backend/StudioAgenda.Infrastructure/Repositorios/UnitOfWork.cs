using StudioAgenda.Domain.Repositorios;

namespace StudioAgenda.Infrastructure.Repositorios;

internal class UnitOfWork : IUnitOfWork
{
    private readonly StudioAgendaDbContext _context;

    public UnitOfWork(StudioAgendaDbContext context)
    {
        _context = context;
    }

    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }
}