using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudioAgenda.Domain.Identidade;
using StudioAgenda.Domain.Repositorios;
using StudioAgenda.Domain.Repositorios.Agenda;
using StudioAgenda.Domain.Repositorios.Profissional;
using StudioAgenda.Domain.Seguranca.SenhaHash;
using StudioAgenda.Domain.Seguranca.Tokens;
using StudioAgenda.Infrastructure.Identidade;
using StudioAgenda.Infrastructure.Repositorios;
using StudioAgenda.Infrastructure.Repositorios.Agenda;
using StudioAgenda.Infrastructure.Repositorios.Cliente;
using StudioAgenda.Infrastructure.Seguranca.SenhaHash;
using StudioAgenda.Infrastructure.Seguranca.Tokens;

namespace StudioAgenda.Infrastructure;

public static class Injecaodependencias
{
    public static void AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
    {
        AddRepositorios(service);
        AddDbContext_SqlServer(service, configuration);
    }

    private static void AddRepositorios(this IServiceCollection service)
    {
        service.AddScoped<IUnitOfWork, UnitOfWork>();
        service.AddScoped<IRegistrarClienteReposirory, ClienteRepository>();
        service.AddScoped<ILeituraClienteRepository, ClienteRepository>();
        service.AddScoped<IRegistrarProfissionalRepository, ProfissionalRepository>();
        service.AddScoped<ILeituraProfissionalRepository, ProfissionalRepository>();
        service.AddScoped<ILeituraAgendaRepository, AgendaRepository>();
        service.AddScoped<IRegistrarAgendaRepository, AgendaRepository>();
        service.AddScoped<ISenhaHash, Argon2SenhaHash>();
        service.AddScoped<IUsuarioLogado, UsuarioLogado>();
    }

    private static void AddDbContext_SqlServer(IServiceCollection service, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SqlServer");

        service.AddDbContext<StudioAgendaDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        
        service.AddScoped<IAccessTokenGernerator>(provider =>
        {
            var expirationTime = configuration.GetValue<uint>("Jwt:ExpirationTimeMinutes");
            var signingKey = configuration.GetValue<string>("Jwt:SigningKey")!;
            
            return new JwtTokenHandler(expirationTime, signingKey);
        });

        
    }
}