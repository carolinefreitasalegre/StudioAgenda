using CommonTestsUtilies.Entidades;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudioAgenda.Domain.Seguranca.SenhaHash;
using StudioAgenda.Infrastructure;
using Testcontainers.MsSql;
using WebApi.Tests.Resourse;

namespace WebApi.Tests;

public class StudioAgendaApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    public ClienteIdentyManager Cliente { get; private set; }
    public ProfissionalIdentityManager Profissional { get; private set; }
    private readonly MsSqlContainer _container;

    public StudioAgendaApplicationFactory()
    {
        _container = new MsSqlBuilder("mcr.microsoft.com/mssql/server:2022-CU10-ubuntu-22.04")
            .Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Tests");

        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<StudioAgendaDbContext>));

            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<StudioAgendaDbContext>(options =>
            {
                options.UseSqlServer(_container.GetConnectionString());
            });
        });
    }

    public async Task InitializeAsync()
    {
        await _container.StartAsync();

        await using var scope = Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<StudioAgendaDbContext>();
        await db.Database.MigrateAsync();
        
        var senhaHash = scope.ServiceProvider.GetRequiredService<ISenhaHash>();
        
        var (profissional, password) = ProfissionalBuilder.Build();
        var (cliente, senha) = ClienteBuilder.Build();

        profissional.Senha = senhaHash.HashSenha(senha);
        cliente.Senha = senhaHash.HashSenha(senha);
        await db.clientes.AddAsync(cliente);
        await db.profissionais.AddAsync(profissional);
        await db.SaveChangesAsync();
        await db.SaveChangesAsync();
        
        Profissional = new ProfissionalIdentityManager(profissional, senha);
        Cliente = new ClienteIdentyManager(cliente, senha);
    }

    public Task DisposeAsync()
    {
        return _container.StopAsync();
    }
}