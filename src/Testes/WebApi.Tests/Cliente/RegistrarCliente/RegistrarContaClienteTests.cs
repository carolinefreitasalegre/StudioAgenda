using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using CommonTestsUtilies.Requisicoes;
using Shouldly;

namespace WebApi.Tests.Cliente.RegistrarCliente;

public class RegistrarClienteTests : IClassFixture<StudioAgendaApplicationFactory>{
     private const string REQUEST_URI = "/Cliente";
     private readonly HttpClient _client;
     
     public RegistrarClienteTests(StudioAgendaApplicationFactory factory)
     {
         _client = factory.CreateClient();
     }
     [Fact]
     public async Task Success()
     {                                                                 
         var request = RequisicaoRegistrarClienteJsonBuilder.Build();
         
         var response = await _client.PostAsJsonAsync(REQUEST_URI, request);
         
         response.StatusCode.ShouldBe(HttpStatusCode.Created);
         await using var responseBody = await response.Content.ReadAsStreamAsync();

         await response.Content.ReadAsStringAsync();
         
         var responseData = await JsonDocument.ParseAsync(responseBody);

         responseData.RootElement.GetProperty("nome").GetString().ShouldBe(request.Nome);
     }

     
    [Fact]
    public async Task Validate_ShouldBeErrorResponse_WhenNameIsEmpty()
    {
        var request = RequisicaoRegistrarClienteJsonBuilder.Build();
        request.Nome = string.Empty;

        var response = await _client.PostAsJsonAsync(REQUEST_URI, request);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        var responseData = await response.Content.ReadFromJsonAsync<JsonElement>();

        var errors = responseData
            .GetProperty("errors")
            .EnumerateArray();

        errors.Count().ShouldBe(1);

        errors.ShouldContain(error =>
            error.GetString() == "Campo nome deve ser preenchido.");
    }


    [Fact]
    public async Task Validate_ShouldBeErrorResponse_WhenPasswordIsTooShort()
    {
        var request = RequisicaoRegistrarClienteJsonBuilder.Build();
        request.Senha = "Ab1!";

        var response = await _client.PostAsJsonAsync(REQUEST_URI, request);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        var responseData = await response.Content.ReadFromJsonAsync<JsonElement>();

        var errors = responseData
            .GetProperty("errors")
            .EnumerateArray();

        errors.Count().ShouldBe(1);

        errors.ShouldContain(error =>
            error.GetString() == "Senha deve conter pelo menos 9 caracteres.");
    }


    [Fact]
    public async Task Validate_ShouldBeErrorResponse_WhenPasswordHasNoNumber()
    {
        var request = RequisicaoRegistrarClienteJsonBuilder.Build();
        request.Senha = "Abcdefghi!";

        var response = await _client.PostAsJsonAsync(REQUEST_URI, request);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        var responseData = await response.Content.ReadFromJsonAsync<JsonElement>();

        var errors = responseData
            .GetProperty("errors")
            .EnumerateArray();

        errors.Count().ShouldBe(1);

        errors.ShouldContain(error =>
            error.GetString() == "A senha deve conter ao menos um número");
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


    [Fact]
    public async Task Validate_ShouldBeErrorResponse_WhenPhoneHasInvalidLength()
    {
        var request = RequisicaoRegistrarClienteJsonBuilder.Build();
        request.Telefone = "1234567890";

        var response = await _client.PostAsJsonAsync(REQUEST_URI, request);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        var responseData = await response.Content.ReadFromJsonAsync<JsonElement>();

        var errors = responseData
            .GetProperty("errors")
            .EnumerateArray();

        errors.Count().ShouldBe(1);

        errors.ShouldContain(error =>
            error.GetString() == "Telefone deve conter 11 caracteres.");
    }


    [Fact]
    public async Task Validate_ShouldBeErrorResponse_WhenPhoneContainsNonNumericCharacters()
    {
        var request = RequisicaoRegistrarClienteJsonBuilder.Build();
        request.Telefone = "1198765432A";

        var response = await _client.PostAsJsonAsync(REQUEST_URI, request);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        var responseData = await response.Content.ReadFromJsonAsync<JsonElement>();

        var errors = responseData
            .GetProperty("errors")
            .EnumerateArray();

        errors.Count().ShouldBe(1);

        errors.ShouldContain(error =>
            error.GetString() == "O campo deve conter apenas números.");
    }

}