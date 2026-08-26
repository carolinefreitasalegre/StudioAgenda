using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using CommonTestsUtilies.Requisicoes;
using Shouldly;
using StudioAgenda.Domain.Dtos.Requisicoes;
using WebApi.Tests.Resourse;


namespace WebApi.Tests.Login.Cliente;

public class LoginClienteComTelefoneTests : IClassFixture<StudioAgendaApplicationFactory>
{
    private const string REQUEST_URI = "/Autenticacao/login-cliente";
    private readonly ClienteIdentyManager _usuario;
    private readonly HttpClient _client;

    public LoginClienteComTelefoneTests(StudioAgendaApplicationFactory factory)
    {
        _client = factory.CreateClient();
        _usuario = factory.Cliente;
    }   

    [Fact]
    public async Task Success()
    {
        var request = new RequisicaoClienteLoginJson
        {
            Telefone = _usuario.PegarTelefone(),
            Senha =  _usuario.PegarSenha()
        };
        var response = await _client.PostAsJsonAsync(REQUEST_URI, request);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        
        await using var responseBody = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBody);
        
        responseData.RootElement.GetProperty("nome").GetString().ShouldBe(_usuario.PegarNome());
        //responseData.RootElement.GetProperty("tokens").GetProperty("accessToken").GetString().ShouldBeEmpty();
    }

    [Fact]
    public async Task ShouldThrowException_WhenUserDontExist()
    {
        var request = RequisicaoLoginClienteComTelefoneJsonBuilder.Build();
        
        var response = await _client.PostAsJsonAsync(REQUEST_URI,  request);
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        
        await using var responseBody= await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBody);
        
        var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();
        var expectedErrorMessaClieteClietege = "Senha ou email inválido";
        
        errors.ShouldSatisfyAllConditions(er =>
        {
            er.Count().ShouldBe(1);
            er.ShouldContain(err=> err.GetString().Equals(expectedErrorMessaClieteClietege));
        });
    }
}