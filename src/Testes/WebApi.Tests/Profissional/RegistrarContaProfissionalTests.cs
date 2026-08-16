using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using CommonTestsUtilies.Requisicoes;
using Shouldly;

namespace WebApi.Tests.Profissional;

public class RegistrarContaProfissionalTests : IClassFixture<StudioAgendaApplicationFactory>
{
    private const string REQUEST_URI = "/Profissional";
    private readonly HttpClient _client;

    public RegistrarContaProfissionalTests(StudioAgendaApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequisicaoRegistrarProfissionalJsonBuilder.Build();
        var response = await _client.PostAsJsonAsync(REQUEST_URI, request);
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var body = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBody);
        
        responseData.RootElement.GetProperty("email").GetString().ShouldBe(request.Email);
    }

    [Fact]
    public async Task Validate_ShouldBeErrorResponse_WhenEmailIsEmpty()
    {
        var request = RequisicaoRegistrarProfissionalJsonBuilder.Build();
        request.Email = string.Empty;
        
        var response = await _client.PostAsJsonAsync(REQUEST_URI, request);
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        
        var responseData = await response.Content.ReadFromJsonAsync<JsonElement>();
        var errors = responseData
            .GetProperty("errors")
            .EnumerateArray();
        
        errors.ShouldContain(error => error.GetString() == "Campo email deve ser preenchido");
    }

    [Fact]
    public async Task Validate_ShouldBeErrorResponse_WhenEmailIsInvalid()
    {
        var request = RequisicaoRegistrarProfissionalJsonBuilder.Build();
        request.Email = "dadossemarroba.com";
        var response = await _client.PostAsJsonAsync(REQUEST_URI, request);
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        
        var responseData = await response.Content.ReadFromJsonAsync<JsonElement>();
        var errors = responseData
            .GetProperty("errors")
            .EnumerateArray();
        
        errors.ShouldContain(error => error.GetString() == "Preencha um email válido.");
    }
    [Fact]
    public async Task Validate_ShouldBeErrorResponse_WhenPasswordHasNoSpecialCharacter()
    {
        var request = RequisicaoRegistrarClienteJsonBuilder.Build();
        request.Senha = "Abcdefghi1";

        var response = await _client.PostAsJsonAsync(REQUEST_URI, request);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        var responseData = await response.Content.ReadFromJsonAsync<JsonElement>();

        var errors = responseData
            .GetProperty("errors")
            .EnumerateArray();

        errors.Count().ShouldBe(1);

        errors.ShouldContain(error =>
            error.GetString() == "A senha deve conter ao menos um caractere especial");
    }
}