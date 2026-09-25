using Microsoft.EntityFrameworkCore;
using StudioAgenda.Domain.Entidades;
using StudioAgenda.Domain.Repositorios.Profissional;

namespace StudioAgenda.Infrastructure.Repositorios.Cliente;

internal class ProfissionalRepository : IRegistrarProfissionalRepository, ILeituraProfissionalRepository
{
    private readonly StudioAgendaDbContext _context;

    public ProfissionalRepository(StudioAgendaDbContext context)
    {
        _context = context;
    }

    public async Task RegistrarProfissional(Profissional profissional)
    {
       await _context.profissionais.AddAsync(profissional);
    }

    public async Task<bool> ExisteProfissionalAtivoEmail(string email)
    {
        return await _context.profissionais.AnyAsync(profissional => profissional.Email.Equals(email));
    }

    public async Task<bool> ExisteProfissionalAtivoId(Guid id)
    {
        return await  _context.profissionais.AnyAsync(profissional => profissional.Id == id);
    }

    public async Task<Profissional?> ObterViaEmail(string email)
    {
        return await _context.profissionais.FirstOrDefaultAsync(profissional => profissional.Email.Equals(email));
    }
   
}