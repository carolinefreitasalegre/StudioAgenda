using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using CommonTestsUtilies.Requisicoes;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using StudioAgenda.Infrastructure;

namespace WebApi.Tests.Agenda;

public class RegistrarAgendaTests : IClassFixture<StudioAgendaApplicationFactory>
{
    private const string REQUEST_URI = "/Agenda";
    private readonly HttpClient _client;
    private readonly StudioAgendaApplicationFactory _factory;

    public RegistrarAgendaTests(StudioAgendaApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Success()
    {

        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<StudioAgendaDbContext>();

        var clientId = Guid.NewGuid();
        var profissionalId = Guid.NewGuid();

        db.clientes.Add(new StudioAgenda.Domain.Entidades.Cliente { Id = clientId});
        db.profissionais.Add(new StudioAgenda.Domain.Entidades.Profissional { Id = profissionalId});
        
        await db.SaveChangesAsync();
        
        var request = RequisicaoRegistrarAgendaJsonBuilder.Build(clientId,  profissionalId);
        var response = await _client.PostAsJsonAsync(REQUEST_URI, request);
        
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        
        await using var responseBody = await response.Content.ReadAsStreamAsync();
        var body = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBody);
        
        responseData.RootElement.GetProperty("servico").GetString().ShouldBe(request.Servico);
    }

    [Fact]
    public async Task Validate_ShouldBeErrorResponse_WhenProfissionalIdIsEmpty()
    {
        var request = RequisicaoRegistrarAgendaJsonBuilder.Build();
        request.ProfissionalId = Guid.Empty;
        
        var response = await _client.PostAsJsonAsync(REQUEST_URI, request);
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        var responseData = await response.Content.ReadFromJsonAsync<JsonElement>();
        
        var errors = responseData.GetProperty("errors").EnumerateArray();
        
        errors.Count().ShouldBe(1);
        errors.ShouldContain(err => err.GetString() == "Selecione a profissionalUseCase desejada.");
    }
}