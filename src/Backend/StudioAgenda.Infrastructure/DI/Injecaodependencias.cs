using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudioAgenda.Domain.Repositorios;
using StudioAgenda.Domain.Seguranca.SenhaHash;
using StudioAgenda.Infrastructure.Repositorios;
using StudioAgenda.Infrastructure.Repositorios.Cliente;
using StudioAgenda.Infrastructure.Seguranca.SenhaHash;

namespace StudioAgenda.Infrastructure;

public static class Injecaodependencias
{
    public static void AddInfrastructure(this IServiceCollection servie, IConfiguration configuration)
    {
        AddRepositorios(servie);
        AddDbContext_SqlServer(servie, configuration);
    }

    private static void AddRepositorios(this IServiceCollection servise)
    {
        servise.AddScoped<IUnitOfWork, UnitOfWork>();
        servise.AddScoped<IRegistrarClienteReposirory, ClienteReposirory>();
        servise.AddScoped<ILeituraClienteRepository, ClienteReposirory>();
        servise.AddScoped<ISenhaHash, Argon2SenhaHash>();
    }

    private static void AddDbContext_SqlServer(IServiceCollection servise, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SqlServer");

        servise.AddDbContext<StudioAgendaDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
    }
}