using Microsoft.EntityFrameworkCore;
using StudioAgenda.Domain.Entidades;

namespace StudioAgenda.Infrastructure;

internal class StudioAgendaDbContext : DbContext
{
    public StudioAgendaDbContext(DbContextOptions options) : base(options) { }
    
    public DbSet<Cliente> clientes { get; set; }
    public DbSet<Profissional> profissionais { get; set; }
    public DbSet<Agenda> agendas { get; set; }
    
}
