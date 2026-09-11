using Microsoft.EntityFrameworkCore;
using StudioAgenda.Domain.Repositorios;

namespace StudioAgenda.Infrastructure.Repositorios.Cliente;

internal class ClienteRepository : IRegistrarClienteReposirory, ILeituraClienteRepository
{
    private readonly StudioAgendaDbContext _context;

    public ClienteRepository(StudioAgendaDbContext context)
    {
        _context = context;
    }
    
    public async Task RegistrarCliente(Domain.Entidades.Cliente cliente) => await _context.AddAsync(cliente);
    
    public async Task<bool> ExisteUsuarioAtivoTelefone(string telefone)
    {
        return await _context.clientes.AnyAsync(cliente => cliente.Telefone.Equals(telefone));
    }
    
    public async Task<bool> ExisteUsuarioAtivoId(Guid id)
    {
        return await _context.clientes.AnyAsync(cliente => cliente.Id == id);
    }

    public async Task<Domain.Entidades.Cliente> ObterViaTelefone(string telefone)
    {
        return await _context.clientes.FirstOrDefaultAsync(cliente => cliente.Telefone.Equals(telefone));
    }
}