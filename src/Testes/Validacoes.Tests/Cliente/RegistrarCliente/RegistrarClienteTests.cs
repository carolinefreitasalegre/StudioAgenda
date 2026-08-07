using CommonTestsUtilies.Requisicoes;
using Shouldly;
using StudioAgenda.Application.Validacoes;

namespace TestesValidacoes.Cliente.RegistrarCliente;

public class RegistrarClienteTests
{
    [Fact]
    public void Success()
    {
        var request = RequisicaoRegistrarUsuarioJsonBuilder.Build();
        var validator = new ValidacaoCliente();
        var result = validator.Validate(request);
        
        Assert.True(result.IsValid);
        result.IsValid.ShouldBeTrue();
        
    }

    [Fact]
    public void Valdate_ShouldHaveError_WhenNameIsEmpty()
    {
        var request = RequisicaoRegistrarUsuarioJsonBuilder.Build();
        request.Nome = string.Empty;
        var validator = new ValidacaoCliente();
        var result = validator.Validate(request);
        
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(err => err.ErrorMessage.Equals("Campo nome deve ser preenchido."));
        });
    }
    
    [Fact]
    public void Validate_ShouldHaveError_WhenTelephoneIsEmpty()
    {
        var request = RequisicaoRegistrarUsuarioJsonBuilder.Build();
        request.Telefone = string.Empty;
        var validator = new ValidacaoCliente();
        var result = validator.Validate(request);
        
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(err => err.ErrorMessage.Equals("Campo telefone deve ser preenchido."));
        });
    }
    
    [Fact]
    public void Validate_ShouldHaveError_WhenPasswordIsEmpty()
    {
        var request = RequisicaoRegistrarUsuarioJsonBuilder.Build();
        request.Senha = string.Empty;
        var validator = new ValidacaoCliente();
        var result = validator.Validate(request);
        
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(err => err.ErrorMessage.Equals("Campo senha deve ser preenchido."));
        });
    }
    
}