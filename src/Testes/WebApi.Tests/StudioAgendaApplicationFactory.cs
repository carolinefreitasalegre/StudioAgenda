using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudioAgenda.Infrastructure;
using Testcontainers.MsSql;

namespace WebApi.Tests;

public class StudioAgendaApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
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

        using var scope = Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<StudioAgendaDbContext>();
        await db.Database.MigrateAsync();
    }

    public Task DisposeAsync()
    {
        return _container.StopAsync();
    }
}